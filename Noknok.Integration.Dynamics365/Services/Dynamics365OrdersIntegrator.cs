using System.Net.Http.Json;
using Catalog.DataLayer.MongoDB.Entities;
using Catalog.Domain.Dtos;
using Catalog.Domain.Interfaces.Repositories;
using Common.Domain.Api;
using Common.Domain.Extensions;
using Common.Domain.Utils;
using Microsoft.Extensions.Options;
using Noknok.Integration.Domain.Interfaces;
using Noknok.Integration.Dynamics365.Models;
using Noknok.Integration.Dynamics365.Settings;

namespace Noknok.Integration.Dynamics365.Services;

internal partial class Dynamics365Integrator(
    IProductRepository productRepository) : IIntegrationHandler<Dynamics365IntegratorSettings>
{
    public async Task MigrateProductDataAsync(Dynamics365IntegratorSettings integrationSettings)
    {
        var categoriesResponse = await GetCategoriesAsync(integrationSettings);
        var marketCategoriesMap = new Dictionary<string, string>();
        while (categoriesResponse is { Succeeded: true, Data: not null })
        {
            marketCategoriesMap.AddRange(categoriesResponse.Data
                .Value.ToDictionary(item => item.ProductNumber, item => item.ProductCategoryName));
            if (string.IsNullOrEmpty(categoriesResponse.Data.OdataNextLink)) break;
            categoriesResponse = await GetCategoriesAsync(integrationSettings, categoriesResponse.Data.OdataNextLink);
        }
        
        var barcodesResponse = await GetBarcodesAsync(integrationSettings);
        var barcodes = new List<BarcodeResponse>();
        while (barcodesResponse is { Succeeded: true, Data: not null })
        {
            barcodes.AddRange(barcodesResponse.Data.Value);
            if(string.IsNullOrEmpty(barcodesResponse.Data.OdataNextLink)) break;
            barcodesResponse = await GetBarcodesAsync(integrationSettings, barcodesResponse.Data.OdataNextLink);
        }

        var barcodesMap = 
            barcodes.GroupBy(b => b.ItemNumber)
                .ToDictionary(grouping => grouping.Key ?? string.Empty, grouping => grouping.Select(b => b.Barcode ?? string.Empty).ToList());

        var productsResponse = await GetProductsAsync(integrationSettings);
        var result = new List<ProductItemDto>();
        while (productsResponse is { Succeeded: true, Data: not null })
        {
            result.AddRange(productsResponse.Data.Value
                .Select(i => i.ToProductItemDto(integrationSettings, marketCategoriesMap, barcodesMap)));
            productsResponse = await GetProductsAsync(integrationSettings, productsResponse.Data.OdataNextLink);
        }
        await productRepository.InsertBulkAsync(result);
    }

    private async Task<ApiResult<ODataResponse<CategoryResponse>>> GetCategoriesAsync(Dynamics365IntegratorSettings integrationSettings, string? url = null)
    {
        var httpClient = await integrationSettings.GenerateHttpClient();
        var apiRoute = url ?? integrationSettings.CategoriesUrl;
        apiRoute = StringUtils.ReplaceCurly(apiRoute.Contains("http") ? apiRoute : $"/{apiRoute}", integrationSettings.RouteParameters);
        var response = await httpClient.GetAsync(apiRoute);
        return new ApiResult<ODataResponse<CategoryResponse>>
        {
            StatusCode = (int)response.StatusCode,
            Succeeded = response.IsSuccessStatusCode,
            Data = await response.Content.ReadFromJsonAsync<ODataResponse<CategoryResponse>>()
        };
    }

    private async Task<ApiResult<ODataResponse<BarcodeResponse>>> GetBarcodesAsync(Dynamics365IntegratorSettings integrationSettings, string? url = null)
    {
        var httpClient = await integrationSettings.GenerateHttpClient();
        var apiRoute = url ?? integrationSettings.BarcodeUrl;
        apiRoute = StringUtils.ReplaceCurly(apiRoute.Contains("http") ? apiRoute : $"/{apiRoute}", integrationSettings.RouteParameters);
        var response = await httpClient.GetAsync(apiRoute);
        return new ApiResult<ODataResponse<BarcodeResponse>>
        {
            StatusCode = (int)response.StatusCode,
            Succeeded = response.IsSuccessStatusCode,
            Data = await response.Content.ReadFromJsonAsync<ODataResponse<BarcodeResponse>>()
        };
    }

    private async Task<ApiResult<ODataResponse<ProductResponse>>> GetProductsAsync(Dynamics365IntegratorSettings integrationSettings, string? url = null)
    {
        var httpClient = await integrationSettings.GenerateHttpClient();
        var apiRoute = url ?? integrationSettings.ItemsUrl;
        apiRoute = StringUtils.ReplaceCurly(apiRoute.Contains("http") ? apiRoute : $"/{apiRoute}", integrationSettings.RouteParameters);
        var response = await httpClient.GetAsync(apiRoute);
        return new ApiResult<ODataResponse<ProductResponse>>
        {
            StatusCode = (int)response.StatusCode,
            Succeeded = response.IsSuccessStatusCode,
            Data = await response.Content.ReadFromJsonAsync<ODataResponse<ProductResponse>>()
        };
    } 
}
