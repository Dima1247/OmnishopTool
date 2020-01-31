using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OmnishopFourSound.DAL.Entities
{
    public class entOmnishopProductSpec
    {
        [Key]
        public int Id { get; set; }
        public int UComProductId { get; set; }
        public int SKU { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public bool IsDisplayed { get; set; }
        public bool IsDeleted { get; set; }
        public int DataTypeId { get; set; }
        public string DataTypeInfo { get; set; }

        public int OmnishopProductId { get; set; }
        public entOmnishopProduct OmnishopProduct { get; set; }
    }
}
