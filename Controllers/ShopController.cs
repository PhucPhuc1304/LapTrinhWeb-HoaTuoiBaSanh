using Microsoft.AspNetCore.Mvc;
using HoaTuoiBaSanh_Core6.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace HoaTuoiBaSanh_Core6.Controllers
{
    public class ShopController : Controller
    {
        private readonly webContext _context = new webContext();

        public IActionResult Index()
        {
            return View();
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
