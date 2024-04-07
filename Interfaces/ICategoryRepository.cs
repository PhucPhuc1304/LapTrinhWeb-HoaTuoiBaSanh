using CF_HOATUOIBASANH.Models;

namespace CF_HOATUOIBASANH.Interface
{
    public interface ICategoryRepository
    {
        IEnumerable<Category> GetAll();
        Category GetById(int id);
        void Add(Category category);
        void Update(Category category);
        void Remove(Category category);

    }
}
