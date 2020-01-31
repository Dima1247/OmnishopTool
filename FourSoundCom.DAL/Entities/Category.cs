using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FourSoundCom.DAL.Entities
{
    [Table("tblCategory")]
    public class Category
    {
        [Key]
        public int Id { get; set; }

        public int CId { get; set; }

        public int Level { get; set; }

        [Required]
        public string Name { get; set; }

        [DataType(DataType.Url)]
        public string Url { get; set; }

        public bool Checked { get; set; }

        public int ExtId { get; set; }

        [ForeignKey("Category")]
        public int? ParentCategoryId { get; set; }

        public Category ParentCategory { get; set; }

        public ICollection<Category> Categories { get; set; }

        public ICollection<ProductCategory> ProductCategories { get; set; }
    }
}
