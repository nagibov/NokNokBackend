using Catalog.Domain.Dtos;

namespace Catalog.Domain.Interfaces.Repositories;

public interface IProductRepository
{
    Task InsertBulkAsync(IEnumerable<ProductItemDto> productItemDtos);
}