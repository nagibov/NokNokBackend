using System.Net.Http.Headers;

namespace Noknok.Integration.Dynamics365.Settings;

public class Dynamics365IntegratorSettings
{
    public string? ItemsUrl { get; set; }
    public string? Token { get; set; }
    public Dictionary<string, string> RouteParameters { get; set; } = new();
    public string? BarcodeUrl { get; set; }
    public string? CategoriesUrl { get; set; }
    public string BaseUrl { get; set; } = string.Empty;

    public HttpClient GenerateHttpClient()
    {
        var client = new HttpClient{BaseAddress = new Uri(BaseUrl)};
        client.DefaultRequestHeaders.Authorization = 
            new AuthenticationHeaderValue("Bearer", Token);
        return client;
    }
}