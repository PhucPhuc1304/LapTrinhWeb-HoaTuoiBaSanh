using HoaTuoiBaSanh_Core6.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;

namespace HoaTuoiBaSanh_Core6.Controllers
{
    public class HomeController : Controller
    {
        private readonly HoaTuoiBaSanhContext _context;

        public HomeController(HoaTuoiBaSanhContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
<<<<<<< Updated upstream
            var newProducts = _context.Products.Where(x => x.ProductStatus.Contains("New")).ToList();
            var saleProducts = _context.Products.Where(x => x.ProductStatus.Contains("Sale")).ToList();
=======
            var newProducts = _context.HangHoas.Where(x => x.TrangThai.Contains("New")).ToList();
            var saleProducts = _context.HangHoas.Where(x => x.TrangThai.Contains("Sale")).ToList();


>>>>>>> Stashed changes
            IndexViewModel viewModel = new IndexViewModel();
            viewModel.SaleProducts = saleProducts;
            viewModel.NewProducts = newProducts;

            return View(viewModel);
        }

    }
}