using CF_HOATUOIBASANH.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CF_HOATUOIBASANH.Areas.Admin.Controllers
{
	[Area("Admin")]
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
			var products = _productRepository.GetAll(); // Lấy danh sách sản phẩm với thông tin loại sản phẩm

			return View(products);
		}
	}
}
