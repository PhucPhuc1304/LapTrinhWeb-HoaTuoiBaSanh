using CF_HOATUOIBASANH.Authencation;
using CF_HOATUOIBASANH.FormatHelper;
using CF_HOATUOIBASANH.Interfaces;
using CF_HOATUOIBASANH.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CF_HOATUOIBASANH.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly ICustomerRepository _customerRepository;

        public CheckoutController(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }
        //[Authorize]
        [CustomAuthorize(Roles = "Admin,User,Manager")]
        public async Task<IActionResult> Index()
        {
            string cartJson = HttpContext.Session.GetString("cart");
            List<CartModel> cart = string.IsNullOrEmpty(cartJson)
                ? new List<CartModel>()
                : JsonConvert.DeserializeObject<List<CartModel>>(cartJson);

            if (cart.Count == 0)
            {
                return RedirectToAction("Index", "Shop");
            }

            double totalPrice = cart.Sum(item => item.Total);

            var serializedAccount = HttpContext.Session.GetString("LoggedInAccount");
            Account account = string.IsNullOrEmpty(serializedAccount)
                ? null
                : JsonConvert.DeserializeObject<Account>(serializedAccount);

            if (account == null)
            {
                return RedirectToAction("Login", "Account");
            }

            Customer customer = await _customerRepository.GetCustomerByAccountIdAsync(account.AccountID);

            if (customer == null)
           {
                //
               return RedirectToAction("Index", "Shop");
            }

            ViewBag.TotalPrice = totalPrice;
            ViewBag.Customer = customer;
            ViewBag.Cart = cart;
            return View();
        }
    }
}
