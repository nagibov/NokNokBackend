using Catalog.Domain.Dtos;
using Catalog.Domain.Enums;

namespace Catalog.DataLayer.MongoDB.Entities;

public class Image
{
    public required string ImageUrl { get; set; }
    public string? ThumbnailUrl { get; set; }
    public bool IsDefault { get; set; }
}

internal class ProductItem : BaseEntity
{
    public string? ThirdPartyUid { get; set; }
    public string? Category { get; set; }
    public List<string>? RecommendationTags { get; set; }
    public List<string>? ProductCollections { get; set; }
    public string? LegacyId { get; set; }
    public string? LegacyProductNumber { get; set; }
    public ProductCharacteristics DefaultProductCharacteristics { get; set; } = new();
    public Dictionary<string, string>? AdditionalProductCharacteristics { get; set; }
    public List<string>? Barcodes { get; set; }
    public DateTime LastModified { get; set; }
    public string? BrandId { get; set; }
    public string? Sku { get; set; }
    public string? Label { get; set; }
    public string? Description { get; set; }
    public List<Image>? Images { get; set; }
    public string? Unit { get; set; }
    public string? ItemViewType { get; set; }
    public bool IsPublished { get; set; }
    public bool NeverRecommend { get; set; }
    public int RecommendationLevel { get; set; }
    public string? GroupId { get; set; }
    public string PromoTag { get; set; }
    public string ReferenceSku { get; set; }
    public ItemType? ItemType { get; set; }
    public bool IsActive { get; set; }
    public int QtyPerUnit { get; set; }
    public int SortOrder { get; set; }
    public int MaxQty { get; set; }
    public int MinQty { get; set; }
    public double Price { get; set; }
    public bool IsDeleted { get; set; }
    public bool VatFree { get; set; }
    public bool RequiresLegalAge { get; set; }
    public bool IsVirtualItem { get; set; }
    public bool IsLocalItem { get; set; }
    public string? ThirdPartyId { get; set; }
    public string? TenantId { get; set; }
    public string? OperationId { get; set; }
    public string? TenantUid { get; set; }
    public string? OperationUid { get; set; }
    public string? ProductRefId { get; set; }
    public List<ProductCustomizationItem> ProductCustomization { get; set; } = [];
    public List<ProductVariationItem> ProductVariation { get; set; } = [];
    public UiCustomization? UiCustomization { get; set; }
}

public class ProductCustomizationItem
{
    public required string Title { get; set; }
    public required List<string> Options { get; set; }
    public bool IsMultiSelect { get; set; }
    public int MinQty { get; set; }
    public int MaxQty { get; set; }
    public bool Required { get; set; }
}

public class ProductVariationItem
{
    public required string Title { get; set; }
    public required List<string> Options { get; set; }
    public bool IsMultiSelect { get; set; }
    public int MinQty { get; set; }
    public int MaxQty { get; set; }
    public bool Required { get; set; }
}

public class UiCustomization
{
    public bool ShowWeight { get; set; }
    public bool IsGiftWrapProduct { get; set; }
    public bool RatingEnabled { get; set; }
}

public class ProductCharacteristics
{
    public int? StockThreshold { get; set; }
    public int? Size { get; set; }
    public int? Weight { get; set; }
    public string? PackagingType { get; set; }
    public int? ItemPreparationTime { get; set; }
}