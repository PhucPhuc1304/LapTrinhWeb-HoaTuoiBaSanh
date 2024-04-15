using CF_HOATUOIBASANH.Interface;
using CF_HOATUOIBASANH.Interfaces;
using CF_HOATUOIBASANH.Models;
using CF_HOATUOIBASANH.Authencation;

using CF_HOATUOIBASANH.Repositorys;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Linq;
using iText.Html2pdf;
using iText.Kernel.Pdf;
using Aspose.Pdf;

namespace CF_HOATUOIBASANH.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[CustomAuthorize(Roles = "Admin")]

    public class OrderController : Controller
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IDetailOrderRepository _orderDetailRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IProductRepository _productRepository;
        private readonly PdfService _pdfService;
        public OrderController(IOrderRepository orderRepository, IDetailOrderRepository orderDetailRepository, ICustomerRepository customerRepository, IProductRepository productRepository, PdfService pdfService)
        {
            _orderRepository = orderRepository;
            _orderDetailRepository = orderDetailRepository;
            _customerRepository = customerRepository;
            _productRepository = productRepository;
            _pdfService = pdfService;
        }
        public IActionResult Index()
        {
            var orders = _orderRepository.GetAllOrders();
            var orderDetails = _orderDetailRepository.GetAllDetailOrders();

            // Grouping order details by OrderID
            var groupedOrderDetails = orderDetails.GroupBy(od => od.OrderID)
                                                  .ToDictionary(g => g.Key, g => g.ToList());

            ViewBag.Orders = orders;
            ViewBag.OrderDetails = groupedOrderDetails;

            return View();
        }


        public async Task<IActionResult> Add()
        {
            ViewBag.Customers = await _customerRepository.GetAllCustomersAsync();
            ViewBag.Products = _productRepository.GetAll();

            return View();
        }
        [HttpPost]
        public IActionResult AddOrder(string customerID, string deliveryMethod, string payMethod, string payStatus, string shipStatus, string shipAddress, string shipCost, string notes, List<OrderDetailViewModel> orderDetails)
        {
            try
            {
                Dictionary<int, int> productQuantities = new Dictionary<int, int>();
                foreach (var detail in orderDetails)
                {
                    int productID = int.Parse(detail.ProductID);
                    int quantity = int.Parse(detail.Quantity);
                    if (productQuantities.ContainsKey(productID))
                    {
                        productQuantities[productID] += quantity;
                    }
                    else
                    {
                        productQuantities[productID] = quantity;
                    }
                }

                decimal totalAmount = 0;
                foreach (var productID in productQuantities.Keys)
                {
                    int quantity = productQuantities[productID];

                    Product product = _productRepository.GetById(productID);
                    if (product != null)
                    {
                        decimal unitPrice = product.Price ?? 0;
                        totalAmount += unitPrice * quantity;
                    }
                }
                totalAmount = totalAmount + decimal.Parse(shipCost);
                Order order = new Order();
                {
                    order.CustomerID = Int32.Parse(customerID);
                    order.CreateDate = DateTime.Now;
                    order.DeliveryMethod = deliveryMethod;
                    order.PayMethod = payMethod;
                    order.PayStatus = payStatus;
                    order.ShipStatus = shipStatus;
                    order.ShipAddress = shipAddress;
                    order.Notes = notes;
                    order.ShipCost = decimal.Parse(shipCost);
                    order.TotalAmount = totalAmount;
                }
                Order newOrder = _orderRepository.CreateOrder(order);

                foreach (var productID in productQuantities.Keys)
                {
                    int quantity = productQuantities[productID];

                    DetailOrder detailOrder = new DetailOrder();
                    {
                        detailOrder.OrderID = newOrder.OrderID;
                        detailOrder.ProductID = productID;
                        detailOrder.Quantity = quantity;
                    }
                    _orderDetailRepository.CreateDetailOrder(detailOrder);
                }

                return Ok("Đơn hàng đã được thêm thành công.");
            }
            catch (Exception ex)
            {
                return BadRequest("Đã xảy ra lỗi khi thêm đơn hàng: " + ex.Message);
            }
        }




        public class OrderDetailViewModel
        {
            public string ProductID { get; set; }
            public string Quantity { get; set; }
        }

        public async Task<IActionResult> Edit(int id)
        {
            var orders = _orderRepository.GetOrderById(id);
            var orderDetails = _orderDetailRepository.GetDetailOrderByIds(id);

            var customersTask = await _customerRepository.GetCustomerByIdAsync(orders.CustomerID);
            ViewBag.Products = _productRepository.GetAll();
            ViewBag.Orders = orders;
            ViewBag.OrderDetails = orderDetails;
            ViewBag.Customers = customersTask;
            return View();
        }
        [HttpPost]
        public ActionResult EditOrder(string customerID, string deliveryMethod, string payMethod, string payStatus, string shipStatus, string shipAddress, string shipCost, string notes, string orderID, List<OrderDetailViewModel> orderDetails)
        {
            Dictionary<int, int> productQuantities = new Dictionary<int, int>();
            foreach (var detail in orderDetails)
            {
                int productID = int.Parse(detail.ProductID);
                int quantity = int.Parse(detail.Quantity);
                if (productQuantities.ContainsKey(productID))
                {
                    productQuantities[productID] += quantity;
                }
                else
                {
                    productQuantities[productID] = quantity;
                }
            }

            decimal totalAmount = 0;
            foreach (var productId in productQuantities.Keys)
            {
                int quantity = productQuantities[productId];

                Product product = _productRepository.GetById(productId);
                if (product != null)
                {
                    decimal unitPrice = product.Price ?? 0;
                    totalAmount += unitPrice * quantity;
                }
            }
            totalAmount = totalAmount + decimal.Parse(shipCost);
            Order order = _orderRepository.GetOrderById(int.Parse(orderID));
            order.CustomerID = int.Parse(customerID);
            order.DeliveryMethod = deliveryMethod;
            order.PayMethod = payMethod;
            order.PayStatus = payStatus;
            order.ShipStatus = shipStatus;
            order.ShipAddress = shipAddress;
            order.ShipCost = decimal.Parse(shipCost);
            order.Notes = notes;
            order.TotalAmount = totalAmount;
            _orderRepository.UpdateOrder(order);

            var detailOrders = _orderDetailRepository.GetDetailOrderByIds(int.Parse(orderID));
            foreach (var productId in productQuantities.Keys)
            {
                var existingDetailOrder = detailOrders.FirstOrDefault(d => d.ProductID == productId);
                if (existingDetailOrder != null)
                {
                    existingDetailOrder.Quantity = productQuantities[productId];
                    _orderDetailRepository.UpdateDetailOrder(existingDetailOrder);
                }
                else
                {
                    var newDetailOrder = new DetailOrder
                    {
                        OrderID = order.OrderID,
                        ProductID = productId,
                        Quantity = productQuantities[productId]
                    };
                    _orderDetailRepository.CreateDetailOrder(newDetailOrder);
                }
            }

            foreach (var existingDetailOrder in detailOrders)
            {
                if (!productQuantities.ContainsKey(existingDetailOrder.ProductID))
                {
                    _orderDetailRepository.DeleteDetailOrder(existingDetailOrder);
                }
            }

            return Json("Order edited successfully");
        }
        public async Task<IActionResult> ExportPDF(int id)
        {
            var order = _orderRepository.GetOrderById(id);
            var orderDetails = _orderDetailRepository.GetDetailOrderByIds(id).ToList();

            // Gọi phương thức GenerateInvoicePdf từ PdfService để tạo tài liệu PDF
            Document pdfDocument = await _pdfService.GenerateInvoicePdf(order, orderDetails);

            // Tạo một MemoryStream để lưu trữ dữ liệu PDF
            using (MemoryStream stream = new MemoryStream())
            {
                // Lưu tài liệu PDF vào MemoryStream
                pdfDocument.Save(stream);

                // Chuyển MemoryStream thành một mảng byte
                byte[] pdfBytes = stream.ToArray();

                // Trả về tài liệu PDF dưới dạng file
                return File(pdfBytes, "application/pdf", "invoice.pdf");
            }
        }




        public IActionResult Delete(int id)
        {
            // Lấy thông tin đơn hàng từ repository
            var order = _orderRepository.GetOrderById(id);
            var orderDetails = _orderDetailRepository.GetDetailOrderByIds(id);

            // Xóa tất cả chi tiết đơn hàng
            foreach (var detail in orderDetails)
            {
                _orderDetailRepository.DeleteDetailOrder(detail);
            }

            // Xóa đơn hàng chính
            _orderRepository.DeleteOrder(id);

            return RedirectToAction("Index", "Order");
        }


    }
}