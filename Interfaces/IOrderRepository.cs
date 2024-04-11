using CF_HOATUOIBASANH.Models;

namespace CF_HOATUOIBASANH.Interfaces
{
    public interface IOrderRepository
    {
        Order CreateOrder(Order order);

        Order GetOrderById(int orderId);

        Order UpdateOrder(Order order);
        void DeleteOrder(int orderId);
    }

}
