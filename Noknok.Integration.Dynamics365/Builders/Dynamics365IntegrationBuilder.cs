using Noknok.Integration.Dynamics365.Services;
using Noknok.Integration.Dynamics365.Settings;

namespace Noknok.Integration.Dynamics365.Builders;

public class Dynamics365IntegrationBuilder
{
    private readonly Dynamics365IntegratorSettings _settings = new();
    
    public Dynamics365IntegrationBuilder WithItemsUrl(string itemsUrl)
    {
        _settings.ItemsUrl = itemsUrl;
        return this;
    }
    
    public Dynamics365IntegrationBuilder WithCategoriesUrl(string categoriesUrl)
    {
        _settings.CategoriesUrl = categoriesUrl;
        return this;
    }

    public Dynamics365IntegrationBuilder WithBarcodeUrl(string barcodeUrl)
    {
        _settings.BarcodeUrl = barcodeUrl;
        return this;
    }

    public Dynamics365IntegrationBuilder WithToken(string token)
    {
        _settings.Token = token;
        return this;
    }

    public Dynamics365IntegrationBuilder WithRouteParameter(string key, string value)
    {
        _settings.RouteParameters.Add(key, value);
        return this;
    }

    public Dynamics365Integrator Build()
    {
        return new Dynamics365Integrator(_settings);
    }
}