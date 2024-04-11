using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CF_HOATUOIBASANH.Models
{
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] 
        public int OrderID { get; set; }

        [Required]
        public int CustomerID { get; set; }

        [StringLength(100)]
        public string? DeliveryMethod { get; set; }

        public DateTime CreateDate { get; set; }

        [StringLength(50)]
        public string? PayMethod { get; set; }


        [StringLength(50)]
        public string? PayStatus { get; set; }

        [StringLength(50)]
        public string? ShipStatus { get; set; }


        [StringLength(255)]
        public string? ShipAddress { get; set; }
        [StringLength(255)]
        public string? Notes { get; set; }

        public decimal? TotalAmount { get; set; }

        [ForeignKey("CustomerID")]
        public Customer Customer { get; set; }
    }
}
