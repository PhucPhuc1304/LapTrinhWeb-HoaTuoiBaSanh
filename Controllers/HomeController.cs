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
            return View(productsByStatus);
        }
        public IActionResult About()
        {
            return View();

        }
        public IActionResult Contact()
        {
            return View();

        }

    }
}