using CF_HOATUOIBASANH.Interface;
using CF_HOATUOIBASANH.Models;
using CF_HOATUOIBASANH.Authencation;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CF_HOATUOIBASANH.Areas.Admin.Controllers
{
	[Area("Admin")]
    //[CustomAuthorize(Roles = "Admin")]

    public class ProductController : Controller
	{
		private readonly IProductRepository _productRepository;
		private readonly ICategoryRepository _categoryRepository;
		public ProductController(IProductRepository productRepository, ICategoryRepository categoryRepository)
		{
			_productRepository = productRepository;
			_categoryRepository = categoryRepository;
		}
		public IActionResult Index()
		{
			var products = _productRepository.GetAll();
			return View(products);
		}
        public IActionResult Add()
        {
            var categories = _categoryRepository.GetAll();
            var categoryList = new SelectList(categories, "CategoryID", "CategoryName");
            ViewBag.Categories = categoryList;
            return View();
        }
        public IActionResult Edit(int id)
        {
            var categories = _categoryRepository.GetAll();
            var categoryList = new SelectList(categories, "CategoryID", "CategoryName");
            ViewBag.Categories = categoryList;
            var products = _productRepository.GetById(id);
            ViewBag.Products = products;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddProduct(int categoryId, string productName, string productUnit, string productStatus, decimal price, decimal? price1, decimal? price2, decimal? price3, IFormFile image, IFormFile image1, IFormFile image2, IFormFile image3, IFormFile image4, string description, string description2, string description3)
        {
            var product = new Product
            {
                CategoryID = categoryId,
                ProductName = productName,
                ProductUnit = productUnit,
                ProductStatus = productStatus,
                Price = price,
                Price1 = price1,
                Price2 = price2,
                Price3 = price3,
                Description = description,
                Description2 = description2,
                Description3 = description3
            };

            if (image != null && image.Length > 0)
            {
                product.Image = await SaveImageAndGetPath(image);
            }
            if (image1 != null && image1.Length > 0)
            {
                product.Image1 = await SaveImageAndGetPath(image1);
            }
            if (image2 != null && image2.Length > 0)
            {
                product.Image2 = await SaveImageAndGetPath(image2);
            }
            if (image3 != null && image3.Length > 0)
            {
                product.Image3 = await SaveImageAndGetPath(image3);
            }
            if (image4 != null && image4.Length > 0)
            {
                product.Image4 = await SaveImageAndGetPath(image4);
            }

            _productRepository.Add(product);

            return RedirectToAction("Index", "Product");
        }


        [HttpPost]
        public async Task<IActionResult> EditProduct(int categoryId, string productName,int productID, string productUnit, string productStatus, decimal price, decimal? price1, decimal? price2, decimal? price3, IFormFile image, IFormFile image1, IFormFile image2, IFormFile image3, IFormFile image4, string description, string description2, string description3)
        {
      
           var existingProduct = _productRepository.GetById(productID); // Lấy thông tin sản phẩm hiện có từ cơ sở dữ liệu
            existingProduct.CategoryID = categoryId;
            existingProduct.ProductName = productName;
            existingProduct.ProductUnit = productUnit;
            existingProduct.ProductStatus = productStatus;
            existingProduct.Price = price;
            existingProduct.Price1 = price1;
            existingProduct.Price2 = price2;
            existingProduct.Price3 = price3;
            existingProduct.Description = description;
            existingProduct.Description2 = description2;
            existingProduct.Description3 = description3;

            if (image != null && image.Length > 0)
            {
                existingProduct.Image = await SaveImageAndGetPath(image);
            }
            if (image1 != null && image1.Length > 0)
            {
                existingProduct.Image1 = await SaveImageAndGetPath(image1);
            }
            if (image2 != null && image2.Length > 0)
            {
                existingProduct.Image2 = await SaveImageAndGetPath(image2);
            }
            if (image3 != null && image3.Length > 0)
            {
                existingProduct.Image3 = await SaveImageAndGetPath(image3);
            }
            if (image4 != null && image4.Length > 0)
            {
                existingProduct.Image4 = await SaveImageAndGetPath(image4);
            }

            _productRepository.Update(existingProduct);

            return RedirectToAction("Index", "Product");

        }


        public IActionResult Delete(int id)
        {
            var productToDelete = _productRepository.GetById(id);

            if (productToDelete == null)
            {
                return NotFound();
            }

            try
            {
                _productRepository.Remove(productToDelete);
                return RedirectToAction("Index", "Product");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        private async Task<string> SaveImageAndGetPath(IFormFile image)
        {
            var uniqueFileName = Guid.NewGuid().ToString() + "_" + image.FileName;
            var imagePath = "/img/product/" + uniqueFileName;

            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/product", uniqueFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await image.CopyToAsync(stream);
            }

            return imagePath;
        }
    }
}
