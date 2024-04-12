using System.Collections.Generic;
using System.Linq;
using CF_HOATUOIBASANH.Interface;
using CF_HOATUOIBASANH.Models;
using Microsoft.EntityFrameworkCore;

namespace CF_HOATUOIBASANH.Repositorys
{
    public class EFProductRepository : IProductRepository
    {
        private readonly HoaTuoiBaSanhContext _context;

        public EFProductRepository(HoaTuoiBaSanhContext context)
        {
            _context = context;
        }

        public IEnumerable<Product> GetAll()
        {
            return _context.Products.Include(p => p.Category).ToList();
        }

        public IEnumerable<Product> GetByStatus()
        {
            return _context.Products.Where(p => p.ProductStatus == "Sale" || p.ProductStatus == "New").ToList();
        }

        public Product GetById(int id)
        {
            return _context.Products.Find(id);
        }

        public void Add(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
        }

        public void Update(Product product)
        {
            _context.Products.Update(product);
            _context.SaveChanges();
        }

        public void Remove(Product product)
        {
            _context.Products.Remove(product);
            _context.SaveChanges();
        }

        public IEnumerable<Product> GetProductsByCategory(int categoryId)
        {
            return _context.Products.Where(p => p.CategoryID == categoryId).ToList();
        }

        public IEnumerable<Product> GetProductsByName(string productName)
        {
            return _context.Products.Where(p => p.ProductName.Contains(productName)).ToList();
        }

        public IEnumerable<Product> GetProductsByPriceRange(decimal minPrice, decimal maxPrice)
        {
            return _context.Products.Where(p => p.Price >= minPrice && p.Price <= maxPrice).ToList();
        }

        public IEnumerable<Product> SearchProducts(string searchString)
        {
            if (!string.IsNullOrEmpty(searchString))
            {
                return _context.Products.Where(p => p.ProductName.Contains(searchString)).ToList();
            }
            else
            {
                return Enumerable.Empty<Product>();
            }
        }

        public IEnumerable<Product> SortProducts(string sortOrder)
        {
            var products = _context.Products.AsQueryable(); 

            switch (sortOrder)
            {
                case "name_desc":
                    return products.OrderByDescending(p => p.ProductName);
                case "Price":
                    return products.OrderBy(p => p.Price);
                case "price_desc":
                    return products.OrderByDescending(p => p.Price);
                default:
                    return products.OrderBy(p => p.ProductName);
            }
        }

    }
}
