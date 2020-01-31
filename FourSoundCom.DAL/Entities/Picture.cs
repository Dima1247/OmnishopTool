using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FourSoundCom.DAL.Entities
{
    [Table("tblPicture")]
    public class Picture
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required, DataType(DataType.ImageUrl)]
        public string Url { get; set; }

        [Required]
        public int Position { get; set; }

        [ForeignKey("Product")]
        public int ProductId { get; set; }

        public Product Product { get; set; }
    }
}
