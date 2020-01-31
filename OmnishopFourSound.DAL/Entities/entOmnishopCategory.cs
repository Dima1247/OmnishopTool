using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace OmnishopFourSound.DAL.Entities
{
    [Table("tblCategories")]
    public class entOmnishopCategory
    {
        [Key]
        public int Id { get; set; }
        public int OmnishopCategoryId { get; set; }
        public int OmnishopParentCategoryId { get; set; }
        public string Name { get; set; }
        public int Level { get; set; }
        public int SortOrder { get; set; }
        public string UrlPart { get; set; }

        public int? ParentCategoryId { get; set; }
        public entOmnishopCategory ParentCategory { get; set; }
        public ICollection<entOmnishopCategory> ChildCategories { get; set; }
        public ICollection<entOmnishopProductCategory> OmnishopProductCategories { get; set; }
    }
}
