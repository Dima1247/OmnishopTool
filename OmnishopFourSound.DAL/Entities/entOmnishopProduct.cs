using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace OmnishopFourSound.DAL.Entities
{
    [Table("tblProducts")]
    public class entOmnishopProduct
    {
        [Key]
        public int Id { get; set; }
        public int UComProductId { get; set; }
        public int Vat { get; set; }

        // OmnishopId/SKU
        public string OmnishopProductId { get; set; }
        // MetaTitle
        public string Name { get; set; }
        public double Price { get; set; }
        public double Cost { get; set; }
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }
        public bool Published { get; set; }
        public string Picture1 { get; set; }
        public string Picture2 { get; set; }
        public string Picture3 { get; set; }

        public bool WIthoutName { get; set; }

        // MetaKeywords
        public int? OmnishopBrandId { get; set; }
        public entOmnishopBrand OmnishopBrand { get; set; }

        // MetaKeywords
        public ICollection<entOmnishopProductCategory> OmnishopProductCategories { get; set; }
        public ICollection<entOmnishopProductSpec> OmnishopProductSpecs { get; set; }

        [NotMapped]
        public string SKU { get; set; }
        [NotMapped]
        public List<string> CatIds { get; set; }

        // Additional and optional:
        //public bool AllowCustomerReviews { get; set; }
        //public int StockQuantity { get; set; }
        //public int MinStockQuantity { get; set; }
        //public bool NotifyAdminForQuantityBelow { get; set; }
        //public int OrderMinimumQuantity { get; set; }
        //public int OrderMaximumQuantity { get; set; }
        //public bool MarkAsNew { get; set; }
        //public double MarkAsNewStartDateTimeUtc { get; set; }
        //public double MarkAsNewEndDateTimeUtc { get; set; }
        //public int Weight { get; set; }
    }
}
