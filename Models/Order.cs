using System;
using System.Collections.Generic;

namespace HoaTuoiBaSanh_Core6.Models
{
    public partial class Order
    {
        public Order()
        {
            DetailOrders = new HashSet<DetailOrder>();
        }

        public int OrderId { get; set; }
        public int? CustomerId { get; set; }
        public string? OrderName { get; set; }
        public DateTime? CreateDate { get; set; }
        public string? PayMethod { get; set; }
        public string? PayStatus { get; set; }
        public decimal? TotalAmount { get; set; }

        public virtual Customer? Customer { get; set; }
        public virtual ICollection<DetailOrder> DetailOrders { get; set; }
    }
}
