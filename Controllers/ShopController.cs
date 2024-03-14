using Microsoft.AspNetCore.Mvc;
using HoaTuoiBaSanh_Core6.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using PagedList;
using Microsoft.EntityFrameworkCore;

namespace HoaTuoiBaSanh_Core6.Controllers
{
    public class ShopController : Controller
    {
        private readonly HoaTuoiBaSanhContext _context = new HoaTuoiBaSanhContext();

        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page, String categoryFilter, string priceFilter)
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


            IQueryable<Product> products = _context.Products; // Use IQueryable<Product> instead of DbSet<Product>
                                                              // Replace '_context.Products' with your actual DbSet property
            var categories = _context.Categories.ToList();
            List<SelectListItem> categoryList = new List<SelectListItem>();
            foreach (var category in categories)
            {
                categoryList.Add(new SelectListItem
                {
                    Value = category.CategoryId.ToString(),
                    Text = category.CategoryName
                });
            }
            ViewBag.CategoryList = categoryList;

            ViewBag.CurrentFilter = searchString;

            // Lọc danh sách hoa dựa trên searchString
            if (!String.IsNullOrEmpty(searchString))
            {
                products = products.Where(p => p.ProductName.Contains(searchString));
            }

            if (categoryFilter != null)
            {
                int categoryIdFilter = int.Parse(categoryFilter);

                var category = _context.Categories
                                        .FirstOrDefault(c => c.CategoryId == categoryIdFilter);

                if (category != null)
                {
                    products = products.Where(p => p.CategoryId == category.CategoryId);
                }
                else
                {
                 
                    products = products.Where(p => false);
                }
            }

            if (!String.IsNullOrEmpty(searchString))
            {
                products = products.Where(p => p.ProductName.Contains(searchString));
            }

            if (!String.IsNullOrEmpty(priceFilter))
            {
                switch (priceFilter)
                {
                    case "0-50":
                        products = products.Where(p => p.Price >= 0 && p.Price <= 20000);
                        break;
                    case "50-100":
                        products = products.Where(p => p.Price > 20000 && p.Price <= 100000);
                        break;
                    case "100-150":
                        products = products.Where(p => p.Price > 10000 && p.Price <= 150000);
                        break;
                    case "150-200":
                        products = products.Where(p => p.Price > 150000 && p.Price <= 200000);
                        break;
                    case "200-250":
                        products = products.Where(p => p.Price > 200000 && p.Price <= 250000);
                        break;
                    case "250":
                        products = products.Where(p => p.Price > 250000);
                        break;
                }
            }


            switch (sortOrder)
            {
                case "name_desc":
                    products = products.OrderByDescending(p => p.ProductName);
                    break;
                case "Price":
                    products = products.OrderBy(p => p.Price);
                    break;
                case "price_desc":
                    products = products.OrderByDescending(p => p.Price);
                    break;
                default:
                    products = products.OrderBy(p => p.ProductName);
                    break;
            }
            Console.WriteLine(_context.Categories);
            ViewBag.CategoryList = _context.Categories.Select(c => new { MaLoai = c.CategoryId, TenLoai = c.CategoryName }).ToList();







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
                cart.Add(new CartModel { Product = _context.Products.FirstOrDefault(p => p.ProductId == int.Parse(id)), Quantity = quantity });
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
                    if (cart[i].Product.ProductId.Equals(id))
                    {
                        return i;
                    }
                }
            }

            return -1;
        }
    }
}
