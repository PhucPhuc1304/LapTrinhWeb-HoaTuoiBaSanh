using CF_HOATUOIBASANH.Models;

namespace CF_HOATUOIBASANH.Interfaces
{
    public interface IDetailOrderRepository
    {
        // Tạo mới một chi tiết đơn hàng
        DetailOrder CreateDetailOrder(DetailOrder detailOrder);

        // Lấy thông tin chi tiết đơn hàng dựa trên ID đơn hàng và ID sản phẩm
        DetailOrder GetDetailOrderByIds(int orderId, int productId);

        // Cập nhật thông tin của một chi tiết đơn hàng
        DetailOrder UpdateDetailOrder(DetailOrder detailOrder);

        // Xóa một chi tiết đơn hàng
        void DeleteDetailOrder(int orderId, int productId);
    }
}
