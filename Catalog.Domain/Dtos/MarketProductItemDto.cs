namespace Catalog.Domain.Dtos;

using System;
using System.Collections.Generic;

public class MarketProductItemDto
{
    public string? Id { get; set; }
    public string? ThirdPartyUid { get; set; }
    public string? Category { get; set; }
    public string? Source { get; set; }
    public DateTime? ModifiedInErpOn { get; set; }
    public List<MarketProductCustomization>? CustomizationItems { get; set; }
    public List<string>? Tags { get; set; }
    public List<string>? RecommendationTags { get; set; }
    public string? LegacyId { get; set; }
    public string? LegacyProductNumber { get; set; }
    public int StockThreshold { get; set; }
    public int Size { get; set; }
    public double? VatPercent { get; set; }
    public DateTime? LastModified { get; set; }
    public string? Zoning { get; set; }
    public string? BrandName { get; set; }
    public string? BrandImage { get; set; }
    public string? BrandId { get; set; }
    public string? SpecialNote { get; set; }
    public string? Currency { get; set; }
    public string? Sku { get; set; }
    public string? Label { get; set; }
    public string? Description { get; set; }
    public string? ImageUrl { get; set; }
    public List<ProductImage>? AdditionalImages { get; set; }
    public string? ThumbnailUrl { get; set; }
    public string? WatermarkUrl { get; set; }
    public string? Unit { get; set; } = "unit";
    public ItemViewType ItemViewType { get; set; } = ItemViewType.Normal;
    public bool? IsPublished { get; set; } = false;
    public string? PromoTag { get; set; }
    public string? ReferenceSku { get; set; }
    public bool? NeverRecommend { get; set; } = false;
    public int? RecommendationLevel { get; set; } = 0;
    public string? SearchKeywords { get; set; }
    public string? Collection { get; set; }
    public string? Group { get; set; } = "";
    public ItemType ItemType { get; set; } = ItemType.Product;
    public List<string>? Barcodes { get; set; }
    public int? Commission { get; set; }
    public string? AdditionalInformation { get; set; }
    public bool? IsVariablePrice { get; set; }
    public double? Weight { get; set; }
    public bool IsActive { get; set; } = true;
    public bool? OutOfStock { get; set; } = false;
    public int? QtyPerUnit { get; set; }
    public int? SortOrder { get; set; } = 100000;
    public int? MaxQty { get; set; }
    public int? MinQty { get; set; }
    public double? Price { get; set; }
    public double? PricePerKg { get; set; }
    public int? DiscountInPercent { get; set; }
    public bool? IsDeleted { get; set; }
    public string? ProductName { get; set; }
    public string? MerchantName { get; set; }
    public List<string>? UnpublishedOnStores { get; set; }
    public List<string>? UnpublishedOnStoreNames { get; set; }
    public double? StrikedPrice { get; set; }
    public double? ActualPrice { get; set; }
    public bool? IsPublishEditable { get; set; }
    public bool IsPriceOverridden { get; set; }
    public string? DiscountLabel { get; set; }
    public string? SqlProductUid { get; set; }
    public string? SqlCategoryLabel { get; set; }
    public string? SqlCategoryUid { get; set; }
    public int? SqlCategoryPosition { get; set; }
    public string? SqlCategoryImageUrl { get; set; }
    public string? SqlSubCategoryLabel { get; set; }
    public string? SqlSubCategoryUid { get; set; }
    public bool VatFree { get; set; } = false;
    public bool RequiresLegalAge { get; set; } = false;
    public bool RatingEnabled { get; set; } = false;
    public bool HasSpecialInstructions { get; set; } = false;
    public string? Temp { get; set; }
    public string? Origin { get; set; }
    public string? Calories { get; set; }
    public string? ShelfLife { get; set; }
    public DateTime UpdatedOn { get; set; } = DateTime.UtcNow;
    public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
    public List<ProductAdditives>? Additives { get; set; }
    public List<ProductAllergens>? Allergens { get; set; }
    public Dictionary<string, Schedule>? Schedules { get; set; }
    public bool HasSchedules => Schedules != null && Schedules.Any();
    public List<ProductCollection>? ProductCollections { get; set; }
    public ProductRating? ProductRating { get; set; }
    public ProductInventory? ProductInventory { get; set; }
    public bool? IsDiscountedFromCampaign { get; set; }
    public bool? WithReminder { get; set; } = false;
    public bool? ShowWeight { get; set; } = false;
    public bool? IsGiftWrapProduct { get; set; } = false;
    public string? Request { get; set; }
    public bool? HasPriceRange { get; set; } = false;
    public double? MaxPriceRange { get; set; }
    public double? MinPriceRange { get; set; }
    public string? ImageData { get; set; }
    public int? Index { get; set; }
    public string? WhishDenominationId { get; set; }
    public bool? IsVirtualItem { get; set; } = false;
    public bool? IsLocalItem { get; set; } = false;
    public bool? IsChristmasDonationItem { get; set; } = false;
    public int? ItemPreparationTime { get; set; } = 0;
    public bool IsBCAMDonationItem { get; set; } = false;
    public bool? IsGiftVoucherItem { get; set; } = false;
    public bool IsBag { get; set; } = false;
    public string? CurrencyCode { get; set; }
    public string? WhishDenomiationId { get; set; }
    public long IdThirdParty { get; set; }
    public long IdTenant { get; set; }
    public string? TenantUid { get; set; }
    public string? OperationUid { get; set; }
    public long? IdOperation { get; set; }
}

public record ProductAllergens
{
    public string? Label { get; set; } = "null";
}

public record ProductAdditives
{
    public string? Label { get; set; } = "null";
}

public enum ItemViewType
{
    Normal = 1,
    Full = 2
}

public enum ItemType
{
    Product,
    Service
}

public class ProductRating
{
    public int ReviewsCount { get; set; } = 0;
    public int Likes { get; set; } = 0;
    public int DisLikes { get; set; } = 0;
}

public class ProductInventory
{
    public int Quantity { get; set; } = 0;
    public int Hold { get; set; } = 0;
    public int Threshold { get; set; } = 0;
    public Bin Bin { get; set; }
}

public class ProductImage
{
    public string? Uid { get; set; }
    public string? Normal { get; set; }
    public string? Thumbnail { get; set; }
}

public class ProductCollection
{
    public string? Id { get; set; }
    public string? Name { get; set; }

    public ProductCollection() { }

    public string? Group { get; set; }

    public bool Deleted { get; set; } = false;
}

public class MarketStoreProductDetails
{
    public string? ProductId { get; set; }
    public string? StoreId { get; set; }
    public string? Id { get; set; }
    public string? ThirdPartyUid { get; set; }
    public string? TenantUid { get; set; }
    public string? OperationUid { get; set; }
    public bool OutOfStock { get; set; } = false;
    public bool? IsPublished { get; set; }
    public double? Price { get; set; }
    public List<string> Stores { get; set; }
}

public class MerchantCityProductDetails
{
    public string? ProductId { get; set; }
    public string? CityId { get; set; }
    public string? Id { get; set; }
    public string? ThirdPartyUid { get; set; }
    public string? TenantUid { get; set; }
    public string? OperationUid { get; set; }
    public string? Sku { get; set; }
    public string? LegacyId { get; set; }
    public double? Price { get; set; }
}
public class Bin
{
    public string? Id { get; set; }
    public string? Name { get; set; }
    public string? FullPath { get; set; }
    public int? Row { get; set; }
    public int? Column { get; set; }
    public string? Zone { get; set; }
    public int? Section { get; set; }
    public string? Color { get; set; }
    public int SortOrder { get; set; } = 0;
}

public class Schedule
{
    public DateTime? Start { get; set; }
    public DateTime? End { get; set; }
    public DateTime? TimeOpen { get; set; }
    public DateTime? TimeClosed { get; set; }
    public bool? IsActive { get; set; }
}

public class MarketProductCustomization
{
    public string? GroupName { get; set; }
    public string? Label { get; set; }
    public string? Description { get; set; }
    public CustomizationType GroupType { get; set; }
    public bool? IsMultipleAllow { get; set; }
    public bool? CanSelectAllItems { get; set; }
    public int? MaxSelection { get; set; }
    public int? MinSelection { get; set; }
    public bool? IsMandatory { get; set; }
    public bool? HasQuantity { get; set; }
    public int? MaxQuantity { get; set; }
    public int? MinQuantity { get; set; }
    public bool? IsFree { get; set; }
    public string? LegacyId { get; set; }
    public List<MarketProductCustomizationItem> Items { get; set; }
    public int Index { get; set; }

    // Transient property, not serialized
    public string? ProductId { get; set; }
}

public enum CustomizationType
{
    RADIO,
    CHECKBOX,
    QUANTITY
}

public class MarketProductCustomizationItem
{
    public CustomizationType Type { get; set; }
    public string? GroupName { get; set; }
    public string? Label { get; set; }
    public string? PriceLabel { get; set; }
    public double? Price { get; set; }
    public string? MaterialId { get; set; }
    public bool? HasQuantity { get; set; }
    public int? MaxQuantity { get; set; }
    public int? MinQuantity { get; set; }
    public string? LegacyId { get; set; }
    public bool? IsIncludedByDefault { get; set; }
}






