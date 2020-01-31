using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace NopVarePreImport.DAL.Entities
{
    public class EntBase
    {
        [Key]
        public int Id { get; set; }
    }

    [Table("tblProduct")]
    public class Product : EntBase
    {
        /// <summary>
        /// External OmnishopProductId
        /// </summary>
        public string OmnishopProductId { get; set; }

        /// <summary>
        /// UProduct key
        /// </summary>
        [ForeignKey("UProduct")]
        public int? UProductId { get; set; }
        public UProduct UProduct { get; set; }

        public ICollection<ProductSupplier> ProductSuppliers { get; set; }
    }

    [Table("tblProductSupplier")]
    public class ProductSupplier : EntBase
    {
        /// <summary>
        /// Product key
        /// </summary>
        [ForeignKey("Product")]
        public int ProductId { get; set; }
        /// <summary>
        /// Supplier key
        /// </summary>
        [ForeignKey("Supplier")]
        public int SupplierId { get; set; }

        /// <summary>
        /// External OmnishopProductSupplierId 
        /// </summary>
        public string ProductSupplierId { get; set; }

        public Product Product { get; set; }
        public Supplier Supplier { get; set; }
    }

    [Table("tblSupplier")]
    public class Supplier : EntBase
    {
        /// <summary>
        /// External OmnishopSupplierId
        /// </summary>
        public string SupplierId { get; set; }
        public string SupplierName { get; set; }

        public ICollection<ProductSupplier> ProductSuppliers { get; set; }
    }

    [Table("tblProductCategory")]
    public class ProductCategory : EntBase
    {
        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }

    [Table("tblCategory")]
    public class Category : EntBase
    {
        /// <summary>
        /// External OmnishopCategoryId
        /// </summary>
        public string OmnishopCategoryId { get; set; }

        [Required]
        public string Name { get; set; }
        public string MenuTitle { get; set; }
        public int Level { get; set; }
        public int SortOrder { get; set; }
        public int ExtId { get; set; }
        public string Path { get; set; }

        public string NodeId { get; set; }

        [ForeignKey("Category")]
        public int? ParentCategoryId { get; set; }
        public Category ParentCategory { get; set; }

        public ICollection<Category> Categories { get; set; }
        public ICollection<ProductCategory> ProductCategories { get; set; }
    }

    [Table("tblUProduct")]
    public class UProduct : EntBase
    {
        /// <summary>
        /// External UcommerceProductId
        /// </summary>
        public string UProductId { get; set; }
        public string PimSKU { get; set; }
        public string ContainerModel { get; set; }
        public string VariantModel { get; set; }
        public string FullModel { get; set; }
        public string DisplayName { get; set; }

        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }

        public double Price { get; set; }
        public double SuggestedRetailPriceIncVat { get; set; }
        
        public int VAT { get; set; }
        public string Currency { get; set; }
        
        public string VendorProductId { get; set; }
        public string ManufacturerProductId { get; set; }

        public string ProductStatusName { get; set; }
        public double PriceSuggestion { get; set; }
        public double PackedWeight { get; set; }
        public DateTime? ModifiedOnUTC { get; set; }

        /// <summary>
        /// Supplier key
        /// </summary>
        [ForeignKey("Product")]
        public int? ProductId { get; set; }
        public Product Product { get; set; }
    }
}
