using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace OmnishopFourSound.DAL.Entities
{
    [Table("tblBrands")]
    public class entOmnishopBrand
    {
        [Key]
        public int Id { get; set; }
        public int OmnishopBrandId { get; set; }
        public string Name { get; set; }
        public string UrlPart { get; set; }

        public ICollection<entOmnishopProduct> OmnishopProducts { get; set; }
    }
}
