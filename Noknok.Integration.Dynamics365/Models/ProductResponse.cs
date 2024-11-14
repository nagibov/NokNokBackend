namespace Noknok.Integration.Dynamics365.Models;
using Catalog.Domain.Dtos;

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
    public double? SalesPrice { get; set; }
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

    public MarketProductItemDto ToProductItemDto(
        dynamic config, 
        dynamic appConfig, 
        Dictionary<string, string> marketCategoriesMap,
        Dictionary<string, List<string>> barcodesMap)
    {
        var marketProductItem = new MarketProductItemDto();
        marketProductItem.IdThirdParty = config.IdThirdParty;
        marketProductItem.IdTenant = config.IdTenant;
        marketProductItem.ThirdPartyUid = config.ThirdPartyUid;
        marketProductItem.TenantUid = config.TenantUid;
        marketProductItem.OperationUid = config.OperationUid;
        marketProductItem.BrandName = this.KDAPPBrand;
        marketProductItem.IdOperation = config.IdOperation;
        marketProductItem.Label = this.KDAppDesc;
        marketProductItem.Description = this.KDAppVariant;
        marketProductItem.CurrencyCode = this.CurrencyCode;

        // TODO: Apply condition for PromotionalItem when it's ready on the ERP (Uncomment and implement the condition)

        if (this.NkPromotionalPrice != null && this.NkPromotionalPrice > 0.0)
            marketProductItem.Price = this.NkPromotionalPrice;
        else
            marketProductItem.Price = this.SalesPrice;

        marketProductItem.StockThreshold = this.KDThreshold;
        marketProductItem.WhishDenomiationId = this.ExternalItemId;
        marketProductItem.IsChristmasDonationItem = this.ProductGroupId?.ToUpper() == "DON ITEMS";
        marketProductItem.IsGiftVoucherItem = this.NKGiftItems?.ToUpper() == "YES";

        marketProductItem.IsVirtualItem = marketProductItem.IsGiftVoucherItem ?? (this.NkDigitalItem?.ToUpper() == "YES");

        // Since every gift voucher is stored locally in stock, we'll forcefully set property to isLocal = true
        marketProductItem.IsLocalItem = marketProductItem.IsGiftVoucherItem ?? (this.NkItemLocalization?.ToUpper() == "LOCAL");
        marketProductItem.ItemPreparationTime = this.NkPreparationTime;
        marketProductItem.IsBCAMDonationItem = this.NkIsDonation?.ToUpper() == "YES";

        if (this.ProductType != null)
        {
            if (this.ProductType.Equals("ITEM", StringComparison.OrdinalIgnoreCase))
                marketProductItem.ItemType = ItemType.Product;
            else if (this.ProductType.Equals("SERVICE", StringComparison.OrdinalIgnoreCase) && marketProductItem.IsVirtualItem == false)
            {
                marketProductItem.ItemType = ItemType.Service;
            }
        }

        double? defaultProductVatPercent = appConfig.OperationConfig?[config.OperationUid]?.DefaultProductVatPercent;

        marketProductItem.Size = this.ProductVolume;
        marketProductItem.VatPercent = (this.SalesSalesTaxItemGroupCode == "ISTG") ? defaultProductVatPercent : 0.0;
        marketProductItem.LegacyId = this.ItemNumber;
        marketProductItem.LegacyProductNumber = this.ProductNumber;
        marketProductItem.IsPublished = this.SHowOnApp != null && this.SHowOnApp.Equals("YES", StringComparison.OrdinalIgnoreCase);
        marketProductItem.Category = marketCategoriesMap[this.ProductNumber];

        var barcodesList = barcodesMap[this.ItemNumber];
        if (barcodesList is { Count: > 0 })
        {
            marketProductItem.Sku = barcodesList[0];
            marketProductItem.Barcodes = barcodesList.Count == 1 ? null : barcodesList;
        }

        var promoTag = string.IsNullOrEmpty(this.KDPromotionalTag) ? null : this.KDPromotionalTag;
        marketProductItem.PromoTag = promoTag;

        marketProductItem.ReferenceSku = this.ReferenceItemId;
        marketProductItem.NeverRecommend = this.KDRecommended != null && this.KDRecommended.Equals("YES", StringComparison.OrdinalIgnoreCase);

        marketProductItem.IsBag = this.NkBags?.ToUpper() == "YES";
        return marketProductItem;
    }
}