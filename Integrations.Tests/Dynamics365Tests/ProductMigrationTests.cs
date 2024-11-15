using Catalog.DataLayer.MongoDB;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Noknok.Integration.Domain.Interfaces;
using Noknok.Integration.Dynamics365;
using Noknok.Integration.Dynamics365.Settings;

namespace Integrations.Tests.Dynamics365Tests;

public class ProductMigrationTests
{
    [Test]
    public async Task WhenMigratingProductsFromDynamics365__ThenItShouldInsertProductsToMongoDb()
    {
        var data = new Dictionary<string, string?>()
        {
            {"MongoDbConfiguration:ConnectionString", "mongodb://localhost:27017/"},
            {"MongoDbConfiguration:DatabaseName", "noknok-testing"},
        };
        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(data)
            .Build();
        var services = new ServiceCollection();
        services.AddCatalogMongoDbDataLayer(configuration)
            .AddDynamics365IntegrationServices();

        var serviceProvider = services.BuildServiceProvider();
        var migratorService = serviceProvider.GetRequiredService<IIntegrationHandler<Dynamics365IntegratorSettings>>();

        await migratorService.MigrateProductDataAsync(new Dynamics365IntegratorSettings
        {
            TenantUid = "XLn2WIxdWx0DBt",
            TenantId = "4021",
            Domain = "iengineeringsal.onmicrosoft.com",
            Authority = "https://login.microsoftonline.com/{0}",
            BarcodeUrl = "data/KDProductBarcodeEntity?$filter=dataAreaId%20eq%20'{areaId}'&cross-company=true",
            BaseUrl = "https://ieng-uat01.sandbox.operations.dynamics.com",
            CategoriesUrl = "data/KDProductCategoryAssignments?cross-company=true&$filter=DataAreaId1%20eq%20'{areaId}'and%20ProductCategoryHierarchyName%20eq%20'233'",
            ClientId = "ca9ac96b-4e45-4aec-8d89-b0e6fd1514ce",
            ClientSecret = "zSesYParzDedrW84+PuJhbqGfP+xbTKu4QAUNyGh7+M=",
            ItemsUrl = "data/KDReleasedDistinctProductsV2?$filter=dataAreaId%20eq%20'{areaId}'%20and%20KDModifiedDateTime%20gt%20{date}&cross-company=true",
            OperationId = "8",
            OperationUid = "eee",
            ThirdPartyId = "4022",
            ThirdPartyUid = "jHn1l1rmHanY3l",
            RouteParameters = new Dictionary<string, object>
            {
                {"areaId", "4022"},
                {"date", DateTime.Now.AddYears(-5)}
            }
        });
    }
}