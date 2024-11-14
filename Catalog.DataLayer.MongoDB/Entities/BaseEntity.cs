using MongoDB.Bson.Serialization.Attributes;

namespace Catalog.DataLayer.MongoDB.Entities;

internal class BaseEntity
{
    [BsonElement("_id")]
    public string Id { get; set; }
    public Guid? CreatedById { get; set; }
    public Guid? LastModifiedById { get; set; }
    public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
    public DateTime? LastModifiedOn { get; set; }
}