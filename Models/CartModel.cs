namespace HoaTuoiBaSanh_Core6.Models
{
    public class CartModel
    {
        public Product Product{ get; set; }
        public int Quantity { get; set; }
        public double Total => (double)(Product.Price * Quantity);
        public double Subtotal { get; set; }
    }
}
