namespace Noknok.Integration.Domain.Interfaces;

public interface IIntegrationHandler<in TSettings> where TSettings : class
{
    Task MigrateProductDataAsync(TSettings settings);
    Task MigrateInventoryDataAsync(TSettings settings);
}