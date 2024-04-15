using System.Collections.Generic;
using CF_HOATUOIBASANH.Models;

namespace CF_HOATUOIBASANH.Interface
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetAll();
        Product GetById(int id);
        void Add(Product product);
        void Update(Product product);
        void Remove(Product product);
        IEnumerable<Product> GetProductsByCategory(int categoryId);
        IEnumerable<Product> GetProductsByName(string productName);
        IEnumerable<Product> GetProductsByPriceRange(decimal minPrice, decimal maxPrice);
        IEnumerable<Product> GetByStatus();
        IEnumerable<Product> SearchProducts(string searchString);
        IEnumerable<Product> SortProducts(string sortOrder);
        IEnumerable<Product> GetRelatedProducts(int productId, int count);


    }
}
