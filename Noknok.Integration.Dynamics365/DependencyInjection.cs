using Microsoft.Extensions.DependencyInjection;
using Noknok.Integration.Domain.Interfaces;
using Noknok.Integration.Dynamics365.Services;
using Noknok.Integration.Dynamics365.Settings;

namespace Noknok.Integration.Dynamics365;

public static class DependencyInjection
{
    public static IServiceCollection AddDynamics365IntegrationServices(this IServiceCollection services)
    {
        services.AddScoped<IIntegrationHandler<Dynamics365IntegratorSettings>, Dynamics365Integrator>();
        return services;
    }
}