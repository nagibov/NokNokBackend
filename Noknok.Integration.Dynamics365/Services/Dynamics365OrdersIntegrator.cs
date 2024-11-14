using System.Net.Http.Json;
using Common.Domain.Api;
using Common.Domain.Extensions;
using Noknok.Integration.Dynamics365.Models;
using Noknok.Integration.Dynamics365.Settings;

namespace Noknok.Integration.Dynamics365.Services;

public partial class Dynamics365Integrator(Dynamics365IntegratorSettings integrationSettings)
{
    public async Task GetOrdersAsync()
    {
        var categoriesResponse = await GetCategoriesAsync();
        var marketCategoriesMap = new Dictionary<string, string>();
        while (categoriesResponse is { Succeeded: true, Data: not null })
        {
            marketCategoriesMap.AddRange(categoriesResponse.Data
                .Value.ToDictionary(item => item.ProductNumber, item => item.ProductCategoryName));
            if (string.IsNullOrEmpty(categoriesResponse.Data.OdataNextLink)) break;
            categoriesResponse = await GetCategoriesAsync(categoriesResponse.Data.OdataNextLink);
        }
        
        var barcodesResponse = await GetBarcodesAsync();
        var barcodes = new List<BarcodeResponse>();
        while (barcodesResponse is { Succeeded: true, Data: not null })
        {
            barcodes.AddRange(barcodesResponse.Data.Value);
            if(string.IsNullOrEmpty(barcodesResponse.Data.OdataNextLink)) break;
            barcodesResponse = await GetBarcodesAsync(barcodesResponse.Data.OdataNextLink);
        }
        
        
    }

    private async Task<ApiResult<ODataResponse<CategoryResponse>>> GetCategoriesAsync(string? url = null)
    {
        var httpClient = integrationSettings.GenerateHttpClient();
        var baseUrl = url ?? integrationSettings.CategoriesUrl;
        baseUrl = integrationSettings.RouteParameters.Values.Aggregate(baseUrl, 
            (current, parameter) => current + $"/{parameter}");
        var response = await httpClient.GetAsync(baseUrl);
        return new ApiResult<ODataResponse<CategoryResponse>>
        {
            StatusCode = (int)response.StatusCode,
            Succeeded = response.IsSuccessStatusCode,
            Data = await response.Content.ReadFromJsonAsync<ODataResponse<CategoryResponse>>()
        };
    }

    private async Task<ApiResult<ODataResponse<BarcodeResponse>>> GetBarcodesAsync(string? url = null)
    {
        var httpClient = integrationSettings.GenerateHttpClient();
        var baseUrl = url ?? integrationSettings.BarcodeUrl;
        baseUrl = integrationSettings.RouteParameters.Values.Aggregate(baseUrl, 
            (current, parameter) => current + $"/{parameter}");
        var response = await httpClient.GetAsync(baseUrl);
        return new ApiResult<ODataResponse<BarcodeResponse>>
        {
            StatusCode = (int)response.StatusCode,
            Succeeded = response.IsSuccessStatusCode,
            Data = await response.Content.ReadFromJsonAsync<ODataResponse<BarcodeResponse>>()
        };
    }

    private async Task<ApiResult<ODataResponse<ProductResponse>>> GetProductsAsync(string? url = null)
    {
        
    } 
}