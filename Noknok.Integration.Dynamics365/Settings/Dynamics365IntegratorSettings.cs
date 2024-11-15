using System.Net.Http.Headers;
using Microsoft.Identity.Client;
using Noknok.Integration.Domain.Interfaces;

namespace Noknok.Integration.Dynamics365.Settings;

public class Dynamics365IntegratorSettings
{
    public string ItemsUrl { get; set; } = string.Empty;
    public Dictionary<string, object> RouteParameters { get; set; } = new();
    public string BarcodeUrl { get; set; } = string.Empty;
    public string CategoriesUrl { get; set; } = string.Empty;
    public string BaseUrl { get; set; } = string.Empty;
    public string? ThirdPartyId { get; set; }
    public string? TenantId { get; set; }
    public string? ThirdPartyUid { get; set; }
    public string? TenantUid { get; set; }
    public string? OperationUid { get; set; }
    public string? OperationId { get; set; }
    public string ClientId { get; set; } = string.Empty;
    public string Domain { get; set; } = string.Empty;
    public string ClientSecret { get; set; } = string.Empty;
    public string Authority { get; set; } = string.Empty;

    public async Task<HttpClient> GenerateHttpClient()
    {
        var client = new HttpClient{BaseAddress = new Uri(BaseUrl)};
        var token = await GenerateToken();
        client.DefaultRequestHeaders.Authorization = 
            new AuthenticationHeaderValue("Bearer", token);
        return client;
    }

    private async Task<string> GenerateToken()
    {
        var authority = string.Format(Authority, TenantId);
        
        // Use Microsoft.Identity.Client (MSAL.NET) for client credential authentication
        var confidentialClientApplication = ConfidentialClientApplicationBuilder.Create(ClientId)
            .WithClientSecret(ClientSecret)
            .WithAuthority(new Uri(authority))
            .Build();

        var result = await confidentialClientApplication.AcquireTokenForClient(new[] { "https://graph.microsoft.com/.default" })
            .ExecuteAsync();

        return result.AccessToken;
    }
}