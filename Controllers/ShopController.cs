using Microsoft.AspNetCore.Mvc;
using HoaTuoiBaSanh_Core6.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using PagedList;

namespace HoaTuoiBaSanh_Core6.Controllers
{
    public class ShopController : Controller
    {
        private readonly webContext _context = new webContext();

        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page, String Loai, string priceFilter)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.PriceSortParm = sortOrder == "Price" ? "price_desc" : "Price";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;


            var products = _context.HangHoas.AsQueryable(); // Replace 'db.Products' with your actual entity set
            var categories = _context.LoaiHangs.ToList();
            List<SelectListItem> categoryList = new List<SelectListItem>();
            foreach (var category in categories)
            {
                categoryList.Add(new SelectListItem
                {
                    Value = category.MaLoai.ToString(),
                    Text = category.TenLoai
                });
            }
            ViewBag.CategoryList = categoryList;

            ViewBag.CurrentFilter = searchString;

            // Lọc danh sách hoa dựa trên searchString
            if (!String.IsNullOrEmpty(searchString))
            {
                products = products.Where(p => p.TenHang.Contains(searchString));
            }

            if (Loai != null)
            {
                products = products.Where(p => p.MaLoai == Loai);
            }
            if (!String.IsNullOrEmpty(searchString))
            {
                products = products.Where(p => p.TenHang.Contains(searchString));
            }

            if (!String.IsNullOrEmpty(priceFilter))
            {
                switch (priceFilter)
                {
                    case "0-50":
                        products = products.Where(p => p.GiaLe >= 0 && p.GiaLe <= 20000);
                        break;
                    case "50-100":
                        products = products.Where(p => p.GiaLe > 20000 && p.GiaLe <= 100000);
                        break;
                    case "100-150":
                        products = products.Where(p => p.GiaLe > 10000 && p.GiaLe <= 150000);
                        break;
                    case "150-200":
                        products = products.Where(p => p.GiaLe > 150000 && p.GiaLe <= 200000);
                        break;
                    case "200-250":
                        products = products.Where(p => p.GiaLe > 200000 && p.GiaLe <= 250000);
                        break;
                    case "250":
                        products = products.Where(p => p.GiaLe > 250000);
                        break;
                }
            }


            switch (sortOrder)
            {
                case "name_desc":
                    products = products.OrderByDescending(p => p.TenHang);
                    break;
                case "Price":
                    products = products.OrderBy(p => p.GiaLe);
                    break;
                case "price_desc":
                    products = products.OrderByDescending(p => p.GiaLe);
                    break;
                default:
                    products = products.OrderBy(p => p.TenHang);
                    break;
            }
            ViewBag.CategoryList = new SelectList(_context.LoaiHangs, "MaLoai", "TenLoai");






            int pageSize = 9; // Number of items to display on each page
            int pageNumber = (page ?? 1);
            var pagedProducts = products.ToPagedList(pageNumber, pageSize);
            return View(pagedProducts);
        }

        [HttpPost]
        public IActionResult AddToCart(string id, int quantity)
        {
            // Retrieve cart from session
            string cartJson = HttpContext.Session.GetString("cart");
            List<CartModel> cart = string.IsNullOrEmpty(cartJson)
                ? new List<CartModel>()
                : JsonConvert.DeserializeObject<List<CartModel>>(cartJson);

            // Check if the product already exists in the cart
            int index = isExist(id, cart);
            if (index != -1)
            {
                // If the product exists, update the quantity
                cart[index].Quantity += quantity;
                HttpContext.Session.SetInt32("count", HttpContext.Session.GetInt32("count").GetValueOrDefault() + 1);
            }
            else
            {
                // If the product doesn't exist, add it to the cart
                cart.Add(new CartModel { SanPham = _context.HangHoas.FirstOrDefault(p => p.MaHang == id), Quantity = quantity });
                // Recalculate the total number of items in the cart
                HttpContext.Session.SetInt32("count", HttpContext.Session.GetInt32("count").GetValueOrDefault() + 1);
            }

            // Update the cart in the session
            HttpContext.Session.SetString("cart", JsonConvert.SerializeObject(cart));

            // The rest of your code, possibly calculating total price or performing other actions
            int cartCount = HttpContext.Session.GetInt32("count").GetValueOrDefault();
            return Json(cartCount);
            // Return a JSON response or perform other actions as needed
        }

        private double CalculateTotalPrice(List<CartModel> cart)
        {
            double totalPrice = 0;

            if (cart != null)
            {
                foreach (CartModel cartItem in cart)
                {
                    totalPrice += cartItem.Total;
                    cartItem.Subtotal = totalPrice;
                }
            }

            return totalPrice;
        }

        private int isExist(string id, List<CartModel> cart)
        {
            if (cart != null)
            {
                for (int i = 0; i < cart.Count; i++)
                {
                    if (cart[i].SanPham.MaHang.Equals(id))
                    {
                        return i;
                    }
                }
            }

            return -1;
        }
    }
}
