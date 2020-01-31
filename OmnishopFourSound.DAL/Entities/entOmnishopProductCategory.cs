using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace OmnishopFourSound.DAL.Entities
{
    [Table("tblProductCategories")]
    public class entOmnishopProductCategory
    {
        [Key]
        public int Id { get; set; }

        public string ProductId { get; set; }
        public string CategoryId { get; set; }

        public int OmnishopProductId { get; set; }
        public int OmnishopCategoryId { get; set; }
        public entOmnishopProduct OmnishopProduct { get; set; }
        public entOmnishopCategory OmnishopCategory { get; set; }
    }
}
