using Catalog.DataLayer.MongoDB.Configurations;
using Catalog.DataLayer.MongoDB.Profiles;
using Catalog.DataLayer.MongoDB.Repositories;
using Catalog.Domain.Interfaces.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Catalog.DataLayer.MongoDB;

public static class DependencyInjection
{
    public static IServiceCollection AddCatalogMongoDbDataLayer(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAutoMapper(cfg => cfg.AddProfile<DtoToEntityMapper>());
        services.AddScoped<IProductRepository, ProductRepository>();
        
        services.Configure<MongoDbConfiguration>(configuration.GetSection(nameof(MongoDbConfiguration)));

        return services;
    }
}