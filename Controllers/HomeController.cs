using CF_HOATUOIBASANH.Interface;
using CF_HOATUOIBASANH.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CF_HOATUOIBASANH.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductRepository _productRepository;

        public HomeController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public IActionResult Index()
        {
            var productsByStatus = _productRepository.GetByStatus();
            // You can pass productsByStatus to the view or further process it as needed
            return View(productsByStatus);
        }

       
    }
}