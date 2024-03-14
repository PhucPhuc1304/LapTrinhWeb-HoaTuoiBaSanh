using System;
using System.Collections.Generic;

namespace HoaTuoiBaSanh_Core6.Models
{
    public partial class Product
    {
        public Product()
        {
            DetailOrders = new HashSet<DetailOrder>();
        }

        public int ProductId { get; set; }
        public int? CategoryId { get; set; }
        public string? ProductName { get; set; }
        public string? ProductUnit { get; set; }
        public string? ProductStatus { get; set; }
        public decimal? Price { get; set; }
        public decimal? Price1 { get; set; }
        public decimal? Price2 { get; set; }
        public decimal? Price3 { get; set; }
        public string? Image { get; set; }
        public string? Image1 { get; set; }
        public string? Image2 { get; set; }
        public string? Image3 { get; set; }
        public string? Image4 { get; set; }
        public string? Description { get; set; }
        public string? Description2 { get; set; }
        public string? Description3 { get; set; }

        public virtual Category? Category { get; set; }
        public virtual ICollection<DetailOrder> DetailOrders { get; set; }
    }
}
