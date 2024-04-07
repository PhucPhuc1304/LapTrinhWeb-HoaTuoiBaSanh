using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CF_HOATUOIBASANH.Models
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] 
        public int ProductID { get; set; }

        [Required]
        public int CategoryID { get; set; }

        [Required]
        [StringLength(100)]
        public string? ProductName { get; set; }

        [StringLength(50)]
        public string? ProductUnit { get; set; }

        [StringLength(50)]
        public string? ProductStatus { get; set; }

        public decimal? Price { get; set; }
        public decimal? Price1 { get; set; }
        public decimal? Price2 { get; set; }
        public decimal? Price3 { get; set; }

        [StringLength(255)]
        public string? Image { get; set; }

        [StringLength(255)]
        public string? Image1 { get; set; }

        [StringLength(255)]
        public string? Image2 { get; set; }

        [StringLength(255)]
        public string? Image3 { get; set; }

        [StringLength(255)]
        public string? Image4 { get; set; }

        public string? Description { get; set; }
        public string? Description2 { get; set; }
        public string? Description3 { get; set; }

        [ForeignKey("CategoryID")]
        public Category Category { get; set; }
    }
}
