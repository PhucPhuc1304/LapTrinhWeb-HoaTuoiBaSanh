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
        private readonly webContext _context;

        public HomeController(webContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var newProducts = _context.HangHoas.Where(x => x.TrangThai.Contains("New")).ToList();
            var saleProducts = _context.HangHoas.Where(x => x.TrangThai.Contains("Sale")).ToList();
                            

            IndexViewModel viewModel = new IndexViewModel();
            viewModel.SaleProducts = saleProducts;
            viewModel.NewProducts = newProducts;

            return View(viewModel);
        }

    }
}