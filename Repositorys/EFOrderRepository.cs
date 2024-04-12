using CF_HOATUOIBASANH.Interfaces;
using CF_HOATUOIBASANH.Models;
using System.Collections.Generic;
using System.Linq;

public class EFOrderRepository : IOrderRepository
{
    private readonly HoaTuoiBaSanhContext _context;

    public EFOrderRepository(HoaTuoiBaSanhContext context)
    {
        _context = context;
    }

    // Phương thức tạo mới một đơn hàng
    public Order CreateOrder(Order order)
    {
        _context.Orders.Add(order);
        _context.SaveChanges();
        return order;
    }

    public Order GetOrderById(int orderId)
    {
        return _context.Orders.FirstOrDefault(o => o.OrderID == orderId);
    }

    // Phương thức lấy tất cả các đơn hàng
    public IEnumerable<Order> GetAllOrders()
    {
        return _context.Orders.ToList();
    }

    public Order UpdateOrder(Order order)
    {
        _context.Orders.Update(order);
        _context.SaveChanges();
        return order;
    }

    // Phương thức xóa một đơn hàng
    public void DeleteOrder(int orderId)
    {
        var orderToDelete = _context.Orders.FirstOrDefault(o => o.OrderID == orderId);
        if (orderToDelete != null)
        {
            _context.Orders.Remove(orderToDelete);
            _context.SaveChanges();
        }
    }
}
