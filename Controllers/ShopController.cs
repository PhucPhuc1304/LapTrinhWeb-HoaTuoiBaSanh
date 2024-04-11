using CF_HOATUOIBASANH.Authencation;
using CF_HOATUOIBASANH.Interface;
using CF_HOATUOIBASANH.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PagedList;

namespace CF_HOATUOIBASANH.Controllers
{
    public class ShopController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        public ShopController(IProductRepository productRepository, ICategoryRepository categoryRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
        }
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

            IEnumerable<Product> products = _productRepository.GetAll();
            var categories = _categoryRepository.GetAll().ToList();
            List<SelectListItem> categoryList = new List<SelectListItem>();
            foreach (var category in categories)
            {
                categoryList.Add(new SelectListItem
                {
                    Value = category.CategoryID.ToString(),
                    Text = category.CategoryName
                });
            }
            ViewBag.CategoryList = categoryList;
            ViewBag.CurrentFilter = searchString;

            if (!String.IsNullOrEmpty(searchString))
            {
                products = _productRepository.SearchProducts(searchString);
            }

            if (categoryFilter != null)
            {
                int categoryIdFilter = int.Parse(categoryFilter);

                var category = _categoryRepository.GetById(categoryIdFilter);
                if (category != null)
                {
                    products = products = _productRepository.GetProductsByCategory(category.CategoryID);
                }
            }

            if (!String.IsNullOrEmpty(searchString))
            {
                products = _productRepository.SearchProducts(searchString);
            }

            if (!String.IsNullOrEmpty(priceFilter))
            {
                switch (priceFilter)
                {
                    case "0-50":
                        products = _productRepository.GetProductsByPriceRange(0, 50000);
                        break;
                    case "50-100":
                        products = _productRepository.GetProductsByPriceRange(50000, 100000);
                        break;
                    case "100-150":
                        products = _productRepository.GetProductsByPriceRange(100000, 150000);
                        break;
                    case "150-200":
                        products = _productRepository.GetProductsByPriceRange(150000, 2000000);
                        break;
                    case "200-250":
                        products = _productRepository.GetProductsByPriceRange(200000, 250000);
                        break;
                    case "250":
                        products = _productRepository.GetProductsByPriceRange(250000, decimal.MaxValue); 
                        break;
                    default:
                        break;
                }
            }
            switch (sortOrder)
            {
                case "name_desc":
                    products = _productRepository.SortProducts("name_desc");
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
            ViewBag.CategoryList = _categoryRepository.GetAll().Select(c => new { MaLoai = c.CategoryID, TenLoai = c.CategoryName }).ToList();

            int pageSize = 6; 
            int pageNumber = (page ?? 1);
            var pagedProducts = products.ToPagedList(pageNumber, pageSize);
            return View(pagedProducts);
        }
        public ActionResult Cart()
        {
            string cartJson = HttpContext.Session.GetString("cart");
            List<CartModel> cart = string.IsNullOrEmpty(cartJson)
                ? new List<CartModel>()
                : JsonConvert.DeserializeObject<List<CartModel>>(cartJson);
            double totalPrice = cart.Sum(item => item.Total);

            ViewBag.TotalPrice = totalPrice;
            return View(cart);
        }

        [HttpPost]
        public IActionResult AddToCart(string id, int quantity)
        {
            
            string cartJson = HttpContext.Session.GetString("cart");
            List<CartModel> cart = string.IsNullOrEmpty(cartJson)
                ? new List<CartModel>()
                : JsonConvert.DeserializeObject<List<CartModel>>(cartJson);



            int index = isExist(id, cart);
            if (index != -1)
            {
                cart[index].Quantity += quantity;
            }
            else
            {
                cart.Add(new CartModel { Product = _productRepository.GetById(Int32.Parse(id)), Quantity = quantity });
                HttpContext.Session.SetInt32("count", HttpContext.Session.GetInt32("count").GetValueOrDefault() + 1);
            }

            HttpContext.Session.SetString("cart", JsonConvert.SerializeObject(cart));

            int cartCount = HttpContext.Session.GetInt32("count").GetValueOrDefault();
            return Json(cartCount);
        }

       

        private int isExist(string id, List<CartModel> cart)
        {
            if (cart != null)
            {
                for (int i = 0; i < cart.Count; i++)
                {
                    if (cart[i].Product.ProductID == Int32.Parse(id))
                    {
                        return i;
                    }
                }
            }
            return -1;
        }

        public IActionResult UpdateCart([FromBody] List<Dictionary<string, int>> cartUpdates)
        {
            if (cartUpdates == null)
            {
                return BadRequest("Cart updates cannot be null.");
            }

            string cartJson = HttpContext.Session.GetString("cart");
            List<CartModel> cart = string.IsNullOrEmpty(cartJson)
                ? new List<CartModel>()
                : JsonConvert.DeserializeObject<List<CartModel>>(cartJson);

            foreach (var update in cartUpdates)
            {
                if (!update.TryGetValue("ProductID", out int productId) || !update.TryGetValue("Quantity", out int quantity))
                {
                    return BadRequest("Invalid cart update format.");
                }

                var cartItem = cart.FirstOrDefault(c => c.Product.ProductID == productId);
                if (cartItem != null)
                {
                    cartItem.Quantity = quantity;
                }
            }
            HttpContext.Session.SetString("cart", JsonConvert.SerializeObject(cart));
            return Ok(); 
        }
        [HttpPost]
        public IActionResult DeleteCartItem(int productId)
        {
            // Lấy danh sách sản phẩm từ Session
            string cartJson = HttpContext.Session.GetString("cart");
            List<CartModel> cart = string.IsNullOrEmpty(cartJson)
                ? new List<CartModel>()
                : JsonConvert.DeserializeObject<List<CartModel>>(cartJson);

            // Tìm và xóa sản phẩm với productId khớp
            var itemToRemove = cart.FirstOrDefault(c => c.Product.ProductID == productId);
            if (itemToRemove != null)
            {
                cart.Remove(itemToRemove);

                // Cập nhật lại danh sách sản phẩm trong Session
                HttpContext.Session.SetString("cart", JsonConvert.SerializeObject(cart));
                HttpContext.Session.SetInt32("count", HttpContext.Session.GetInt32("count").GetValueOrDefault() - 1);


                // Trả về kết quả thành công
                return Ok();
            }
            else
            {
                return BadRequest("Product not found in cart.");
            }
        }
    }
}
