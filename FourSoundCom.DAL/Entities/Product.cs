using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FourSoundCom.DAL.Entities
{
    [Table("tblProduct")]
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public string Sku { get; set; }
        public string Name { get; set; }
        public string Brand { get; set; }
        public string URL { get; set; }
        public string Price { get; set; }
        public bool Checked { get; set; }
        public string Description { get; set; }
        public string JsonSource { get; set; }

        public ICollection<ProductCategory> ProductCategories { get; set; }
        public ICollection<Picture> Pictures { get; set; }
    }
}
