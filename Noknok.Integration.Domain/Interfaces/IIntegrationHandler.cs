namespace Noknok.Integration.Domain.Interfaces;

public interface IIntegrationHandler
{
    Task MigrateProductDataAsync();
}