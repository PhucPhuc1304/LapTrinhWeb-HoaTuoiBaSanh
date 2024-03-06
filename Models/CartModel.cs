namespace HoaTuoiBaSanh_Core6.Models
{
    public class CartModel
    {
        public HangHoa SanPham { get; set; }
        public int Quantity { get; set; }
        public double Total => (double)(SanPham.GiaLe * Quantity);
        public double Subtotal { get; set; }
    }
}
