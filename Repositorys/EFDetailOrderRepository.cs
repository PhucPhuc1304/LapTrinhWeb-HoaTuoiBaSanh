using CF_HOATUOIBASANH.Interfaces;
using CF_HOATUOIBASANH.Models;
using Microsoft.EntityFrameworkCore;

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
            return _context.DetailOrders.Include(d => d.Product).ToList();
        }

        public DetailOrder CreateDetailOrder(DetailOrder detailOrder)
        {
            _context.DetailOrders.Add(detailOrder);
            _context.SaveChanges();
            return detailOrder;
        }

        public IEnumerable<DetailOrder> GetDetailOrderByIds(int orderId)
        {
            return _context.DetailOrders.Where(d => d.OrderID == orderId).ToList();
        }

        public DetailOrder UpdateDetailOrder(DetailOrder detailOrder)
        {
            _context.DetailOrders.Update(detailOrder);
            _context.SaveChanges();
            return detailOrder;
        }

        public void DeleteDetailOrder(DetailOrder detailOrder)
        {
            _context.DetailOrders.Remove(detailOrder);
            _context.SaveChanges();
        }
    }
}
