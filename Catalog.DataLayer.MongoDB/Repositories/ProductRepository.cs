using AutoMapper;
using Catalog.DataLayer.MongoDB.Configurations;
using Catalog.DataLayer.MongoDB.Entities;
using Catalog.Domain.Dtos;
using Catalog.Domain.Interfaces.Repositories;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Catalog.DataLayer.MongoDB.Repositories;

internal class ProductRepository : IProductRepository
{
    private readonly IMapper _mapper;
    private readonly IMongoCollection<ProductItem> _productCollection;
    private const string ProductCollectionName = "ProductItems";

    public ProductRepository(IMapper mapper, IOptions<MongoDbConfiguration> options)
    {
        _mapper = mapper;
        var client = new MongoClient(options.Value.ConnectionString);
        var database = client.GetDatabase(options.Value.DatabaseName);
        _productCollection = database.GetCollection<ProductItem>(ProductCollectionName);
    }

    public async Task InsertBulkAsync(IEnumerable<ProductItemDto> productItemDtos)
    {
        var operations = productItemDtos.Select(item =>
        {
            var productItem = _mapper.Map<ProductItem>(item);
            var legacyIdFilter = Builders<ProductItem>.Filter
                .Eq(x => x.LegacyId, productItem.LegacyId);
            var thirdPartyIdFilter = Builders<ProductItem>.Filter
                .Eq(x => x.ThirdPartyId, productItem.ThirdPartyId);
            var filter = Builders<ProductItem>.Filter.And(legacyIdFilter, thirdPartyIdFilter);
            return new ReplaceOneModel<ProductItem>(filter, productItem){ IsUpsert = true };
        });
        await _productCollection.BulkWriteAsync(operations);
    }
}