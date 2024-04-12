using CF_HOATUOIBASANH.Interfaces;
using CF_HOATUOIBASANH.Models;

namespace CF_HOATUOIBASANH.Repositorys
{
    public class EFDetailOrderRepository : IDetailOrderRepository
    {
        private readonly HoaTuoiBaSanhContext _context;

        public EFDetailOrderRepository(HoaTuoiBaSanhContext context)
        {
            _context = context;
        }
        public IEnumerable<DetailOrder> GetAllDetailOrders()
        {
            return _context.DetailOrders.ToList();
        }

        public DetailOrder CreateDetailOrder(DetailOrder detailOrder)
        {
            _context.DetailOrders.Add(detailOrder);
            _context.SaveChanges();
            return detailOrder;
        }

        public DetailOrder GetDetailOrderByIds(int orderId, int productId)
        {
            return _context.DetailOrders.FirstOrDefault(d => d.OrderID == orderId && d.ProductID == productId);
        }

        public DetailOrder UpdateDetailOrder(DetailOrder detailOrder)
        {
            _context.DetailOrders.Update(detailOrder);
            _context.SaveChanges();
            return detailOrder;
        }

        public void DeleteDetailOrder(int orderId, int productId)
        {
            var detailOrderToDelete = _context.DetailOrders.FirstOrDefault(d => d.OrderID == orderId && d.ProductID == productId);
            if (detailOrderToDelete != null)
            {
                _context.DetailOrders.Remove(detailOrderToDelete);
                _context.SaveChanges();
            }
        }
    }
}
