using Microsoft.Extensions.DependencyInjection;
using Noknok.Integration.Domain.Interfaces;
using Noknok.Integration.Dynamics365.Services;

namespace Noknok.Integration.Dynamics365;

public static class DependencyInjection
{
    public static IServiceCollection AddDynamics365IntegrationServices(this IServiceCollection services)
    {
        services.AddScoped<IIntegrationHandler, Dynamics365Integrator>();
        return services;
    }
}