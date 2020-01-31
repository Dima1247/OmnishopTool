using System;
using System.Collections.Generic;
using System.Text;

namespace FourSoundCom.DAL
{
    public class ProductFromSite
    {
        public int ProductId { get; set; }
        public string ProductSKU { get; set; }
        public string ProductVariantSku { get; set; }
        public string FormattedVariantSku { get; set; }
        public string ProductUrl { get; set; }
        public string ProductTitle { get; set; }
        public string ProductGroup { get; set; }
        public string ProductRating { get; set; }
        public int ProductRatingCount { get; set; }
        public bool IsVariantOnline { get; set; }
        public string ProductPrice { get; set; }
        public string OldPrice { get; set; }
        public string OldPricePercentage { get; set; }
        public string ImageUrl { get; set; }
        public string BigImageUrl { get; set; }
        public bool IsHasBadge { get; set; }
        public string BadgeUrl { get; set; }
        public string DiscountText { get; set; }
        public string LongDiscountText { get; set; }
        public string ShortDescription { get; set; }
        public int Rating { get; set; }
        public bool WithGallery { get; set; }
        public string GalleryId { get; set; }
        public string SeePhotosText { get; set; }
        public int CatalogId { get; set; }
        public string ImageAlt { get; set; }
        public bool IsHasRating { get; set; }
        public int WebShopStatus { get; set; }
        public int StockStatus { get; set; }
        public string InStockText { get; set; }
        public bool IsStateDemoDeal { get; set; }
        public string VariantRating { get; set; }
        public string Brand { get; set; }
        public string ProductName { get; set; }
        public bool IsNotify { get; set; }
        public bool IsPreOrdering { get; set; }
        public bool IsInWishList { get; set; }
        public string[] GalleryItemsPath { get; set; }
        public object Category { get; set; }
    }
}
