using CF_HOATUOIBASANH.Interface;
using CF_HOATUOIBASANH.Models;

namespace CF_HOATUOIBASANH.Repositorys
{
    public class EFCategoryRepository : ICategoryRepository
    {
        private readonly HoaTuoiBaSanhContext _context;

        public EFCategoryRepository(HoaTuoiBaSanhContext context)
        {
            _context = context;
        }

        public IEnumerable<Category> GetAll()
        {
            return _context.Categories.ToList();
        }

        public Category GetById(int id)
        {
            return _context.Categories.Find(id);
        }

        public void Add(Category category)
        {
            _context.Categories.Add(category);
            _context.SaveChanges();
        }

        public void Update(Category category)
        {
            _context.Categories.Update(category);
            _context.SaveChanges();
        }

        public void Remove(Category category)
        {
            _context.Categories.Remove(category);
            _context.SaveChanges();
        }



    }
}
