using Catalog.Domain.Dtos;
using Catalog.Domain.Enums;
using Noknok.Integration.Dynamics365.Settings;

namespace Noknok.Integration.Dynamics365.Models;

public class ProductResponse
{
    public string ItemNumber { get; set; } = string.Empty;
    public string ProductNumber { get; set; } = string.Empty;
    public string? KDAPPBrand { get; set; }
    public string? KDAppDesc { get; set; }
    public string? KDPromotionalTag { get; set; }
    public string? KDRecommended { get; set; }
    public string? ReferenceItemId { get; set; }
    public string? ProductType { get; set; }
    public string? KDAppVariant { get; set; }
    public double SalesPrice { get; set; }
    public int KDThreshold { get; set; } = 0;  
    public int ProductVolume { get; set; } = 0;  
    public string? SalesSalesTaxItemGroupCode { get; set; }
    public DateTime? KDModifiedDateTime { get; set; }
    public string? SHowOnApp { get; set; }
    public string? NkDigitalItem { get; set; }
    public string? ProductGroupId { get; set; }
    public string? NkItemLocalization { get; set; }
    public string? NKGiftItems { get; set; }
    public string ExternalItemId { get; set; }
    public string KDPromotionalItem { get; set; }
    public double? NkPromotionalPrice { get; set; }
    public string? CurrencyCode { get; set; }
    public string? NkBags { get; set; }
    public int? NkPreparationTime { get; set; } = 0;  
    public string? NkIsDonation { get; set; }

    public ProductItemDto ToProductItemDto(
        Dynamics365IntegratorSettings config, 
        Dictionary<string, string> marketCategoriesMap,
        Dictionary<string, List<string>> barcodesMap)
    {
        var marketProductItem = new ProductItemDto
        {
            IdThirdParty = config.ThirdPartyId,
            TenantId = config.TenantId,
            ThirdPartyUid = config.ThirdPartyUid,
            TenantUid = config.TenantUid,
            OperationUid = config.OperationUid,
            OperationId = config.OperationId,
            Label = KDAppDesc,
            Description = KDAppVariant,
            BrandId = KDAPPBrand,
            // TODO: Apply condition for PromotionalItem when it's ready on the ERP (Uncomment and implement the condition)
            Price = NkPromotionalPrice is > 0.0 ? 
                NkPromotionalPrice.Value : SalesPrice,
            IsVirtualItem = NkDigitalItem?.Equals("YES", StringComparison.OrdinalIgnoreCase) ?? false,
            // Since every gift voucher is stored locally in stock, we'll forcefully set property to isLocal = true
            IsLocalItem = NkItemLocalization?.Equals("LOCAL", StringComparison.OrdinalIgnoreCase) ?? false,
            DefaultProductCharacteristics =
            {
                ItemPreparationTime = NkPreparationTime
            }
        };
        
        if (ProductType != null)
        {
            if (ProductType.Equals("ITEM", StringComparison.OrdinalIgnoreCase))
                marketProductItem.ItemType = ItemType.Product;
            else if (ProductType.Equals("SERVICE", StringComparison.OrdinalIgnoreCase) && marketProductItem.IsVirtualItem == false)
                marketProductItem.ItemType = ItemType.Service;
        }
        
        marketProductItem.DefaultProductCharacteristics.Size = ProductVolume;
        marketProductItem.VatFree = !SalesSalesTaxItemGroupCode?.Equals("ISTG", StringComparison.OrdinalIgnoreCase) ?? true;
        marketProductItem.LegacyId = ItemNumber;
        marketProductItem.LegacyProductNumber = ProductNumber;
        marketProductItem.IsPublished = SHowOnApp != null && SHowOnApp.Equals("YES", StringComparison.OrdinalIgnoreCase);
        marketProductItem.Category = marketCategoriesMap[ProductNumber];

        var barcodesList = barcodesMap[ItemNumber];
        if (barcodesList is { Count: > 0 })
        {
            marketProductItem.Sku = barcodesList[0];
            marketProductItem.Barcodes = barcodesList.Count == 1 ? null : barcodesList;
        }

        var promoTag = string.IsNullOrEmpty(KDPromotionalTag) ? null : KDPromotionalTag;
        marketProductItem.PromoTag = promoTag;

        marketProductItem.ReferenceSku = ReferenceItemId;
        marketProductItem.NeverRecommend = KDRecommended != null && KDRecommended.Equals("YES", StringComparison.OrdinalIgnoreCase);

        marketProductItem.DefaultProductCharacteristics.PackagingType = NkBags is not null && NkBags.Equals("YES", StringComparison.OrdinalIgnoreCase) ? "BAG" : "";
        return marketProductItem;
    }
}