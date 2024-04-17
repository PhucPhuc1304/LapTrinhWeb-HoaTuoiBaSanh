using CF_HOATUOIBASANH.Authencation;
using CF_HOATUOIBASANH.Interface;
using CF_HOATUOIBASANH.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CF_HOATUOIBASANH.Areas.Admin.Controllers
{
    [Area("Admin")]
    

    public class HomeController : Controller
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IDetailOrderRepository _detailOrderRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IProductRepository _productRepository;

        public HomeController(IOrderRepository orderRepository,IDetailOrderRepository detailOrderRepository,ICustomerRepository customerRepository, IProductRepository productRepository)
        {
            _orderRepository = orderRepository;
            _detailOrderRepository = detailOrderRepository;
            _customerRepository = customerRepository;
            _productRepository = productRepository;
        }
		[CustomAuthorize(Roles = "Admin,Manager")]
		public async Task<IActionResult> Index()
        {
            var customers = await _customerRepository.GetAllCustomersAsync();
            var customerCount = customers.Count();
            ViewBag.CustomerCount = customerCount;


            var orders = _orderRepository.GetAllOrders();
            var totalOrders = orders.Count();
            var totalAmount = orders.Sum(order => order.TotalAmount);
            ViewBag.TotalOrders = totalOrders;
            ViewBag.TotalAmount = totalAmount;

            var detailOrders = _detailOrderRepository.GetAllDetailOrders();
            var topProducts = detailOrders
                .GroupBy(detailOrder => detailOrder.ProductID)
                .Select(group => new
                {
                    ProductID = group.Key,
                    TotalQuantity = group.Sum(detailOrder => detailOrder.Quantity)
                })
                .OrderByDescending(product => product.TotalQuantity)
                .Take(4) 
                .ToList();

            var topProductsWithImages = new List<object>();
            foreach (var product in topProducts)
            {
                var productFromDatabase = _productRepository.GetById(product.ProductID);
                if (productFromDatabase != null)
                {
                    topProductsWithImages.Add(new
                    {
                        ProductID = product.ProductID,
                        TotalQuantity = product.TotalQuantity,
                        ImageUrl = productFromDatabase.Image,
                        Name = productFromDatabase.ProductName
                    });
                }
            }

            ViewBag.TopProductsWithImages = topProductsWithImages;

			var topRevenueProducts = detailOrders
				.GroupBy(detailOrder => detailOrder.ProductID)
				.Select(group => new
				{
					ProductID = group.Key,
					TotalRevenue = group.Sum(detailOrder => detailOrder.Quantity * (group.FirstOrDefault().Product?.Price ?? 0)) // Lấy giá từ Product
				})
				.OrderByDescending(product => product.TotalRevenue)
				.Take(2)
				.ToList();
			ViewBag.TopRevenueProducts = topRevenueProducts;

			var topRevenueProductsWithImages = new List<object>();
			foreach (var product in topRevenueProducts)
			{
				var productFromDatabase = _productRepository.GetById(product.ProductID);
				if (productFromDatabase != null)
				{
					// Tính tổng số lượng sản phẩm từ tất cả các chi tiết đơn hàng của sản phẩm
					var totalQuantity = detailOrders
						.Where(detailOrder => detailOrder.ProductID == product.ProductID)
						.Sum(detailOrder => detailOrder.Quantity);

					topRevenueProductsWithImages.Add(new
					{
						ProductID = product.ProductID,
						TotalRevenue = product.TotalRevenue,
						TotalQuantity = totalQuantity,
						ImageUrl = productFromDatabase.Image,
						Name = productFromDatabase.ProductName
					});
				}
			}

            var today = DateTime.Today;
            var startOfWeek = today.AddDays(-(int)today.DayOfWeek + (int)DayOfWeek.Monday);

            var ordersThisWeek = orders.Where(order => order.CreateDate.Date >= startOfWeek.Date && order.CreateDate.Date <= today.Date);

            var ordersByDayOfWeek = ordersThisWeek
                .GroupBy(order => order.CreateDate.DayOfWeek)
                .Select(group => new
                {
                    DayOfWeek = group.Key,
                    TotalOrders = group.Count()
                })
                .OrderBy(group => group.DayOfWeek)
                .ToList();

            var orderedDaysOfWeek = new List<DayOfWeek> { DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Wednesday, DayOfWeek.Thursday, DayOfWeek.Friday, DayOfWeek.Saturday, DayOfWeek.Sunday };
            var ordersByDayOfWeekFormatted = orderedDaysOfWeek
                .Select(dayOfWeek => new
                {
                    Day = GetDayOfWeekName(dayOfWeek),
                    Sell = ordersByDayOfWeek.FirstOrDefault(group => group.DayOfWeek == dayOfWeek)?.TotalOrders ?? 0
                })
                .ToList();

            var formattedResult = ordersByDayOfWeekFormatted.Select(item => new
            {
                day = item.Day,
                sell = item.Sell
            }).ToList();

            ViewBag.OrdersByDayOfWeek = formattedResult;
            ViewBag.TopRevenueProductsWithImages = topRevenueProductsWithImages;
			ViewBag.TopRevenueProductsWithImages = topRevenueProductsWithImages;

            return View();
        }
        private string GetDayOfWeekName(DayOfWeek dayOfWeek)
        {
            switch (dayOfWeek)
            {
                case DayOfWeek.Sunday:
                    return "Chủ nhật";
                case DayOfWeek.Monday:
                    return "Thứ 2";
                case DayOfWeek.Tuesday:
                    return "Thứ 3";
                case DayOfWeek.Wednesday:
                    return "Thứ 4";
                case DayOfWeek.Thursday:
                    return "Thứ 5";
                case DayOfWeek.Friday:
                    return "Thứ 6";
                case DayOfWeek.Saturday:
                    return "Thứ 7";
                default:
                    return "";
            }
        }

    }

}
