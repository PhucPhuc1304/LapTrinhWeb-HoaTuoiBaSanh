using CF_HOATUOIBASANH.Interfaces;
using CF_HOATUOIBASANH.Models;
using CF_HOATUOIBASANH.Repositorys;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Security.Policy;

namespace CF_HOATUOIBASANH.Controllers
{
    public class PaymentController : Controller
    {

        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IVNPayRepository _vnPayRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IDetailOrderRepository _detailOrderRepository;
        private readonly ICustomerRepository _customerRepository;


        public PaymentController(IConfiguration configuration, IHttpClientFactory httpClientFactory, IVNPayRepository vnPayRepository, IOrderRepository orderRepository, IDetailOrderRepository detailOrderRepository, ICustomerRepository customerRepository)
        {
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
            _vnPayRepository = vnPayRepository;
            _orderRepository = orderRepository;
            _detailOrderRepository = detailOrderRepository;
            _customerRepository = customerRepository;
        }


        public async Task<IActionResult> ProcessPayment(string lastName, string firstName, string phone, string email, string note, string result, string paymentMethod, string deliveryMethod, string typePayment, bool useShip, double totalPrice)
        {
            var total = totalPrice;
            var url = "";
            DateTime currentTime = DateTime.Now;

            if (useShip)
            {
                try
                {
                    var shipController = new ShipController(_configuration, _httpClientFactory);
                    var shipCostResponse = await shipController.ShipCost(firstName, phone, result);
                    var shipCostResponseObjectResult = shipCostResponse as OkObjectResult;
                    var ship_ = shipCostResponseObjectResult.Value;
                    double shippingPrice = Convert.ToDouble(ship_);
                    total += shippingPrice;

                }
                catch (Exception ex)
                {

                    return StatusCode(500, $"An error occurred: {ex.Message}");
                }
            }
            if(typePayment == "VnPay")
            {

                string message = $"Thanh toán cho đơn hàng {currentTime}";
                PaymentInformationModel newPaymentInfo = new PaymentInformationModel
                {
                    OrderType = typePayment,
                    Amount = total,
                    OrderDescription = message,
                    Name = lastName,
                    DeliveryMethod = deliveryMethod,
                    Note = note,
                    ShipAddress = result,
                };



                Console.WriteLine(newPaymentInfo);
                url = CreatePaymentUrl(newPaymentInfo);

            }

          
            if (typePayment == "COD")
            {
                string cartJson = HttpContext.Session.GetString("cart");
                List<CartModel> cart = string.IsNullOrEmpty(cartJson)
                    ? new List<CartModel>()
                    : JsonConvert.DeserializeObject<List<CartModel>>(cartJson);

                string serializedAccount = HttpContext.Session.GetString("LoggedInAccount");
                Account account = JsonConvert.DeserializeObject<Account>(serializedAccount);

                Customer customer = await _customerRepository.GetCustomerByAccountIdAsync(account.AccountID);

                Order order = new Order
                {
                    CustomerID = customer.CustomerID,
                    DeliveryMethod = deliveryMethod,
                    CreateDate = DateTime.Now,
                    PayMethod = "COD",
                    PayStatus = "Pending",
                    ShipStatus = "Pending",
                    Notes = note != "" ? note : null,
                    ShipAddress = result,
                    TotalAmount = (decimal?)total 
                };

                Order createdOrder = _orderRepository.CreateOrder(order);

                foreach (var cartItem in cart)
                {
                    DetailOrder detailOrder = new DetailOrder
                    {
                        OrderID = createdOrder.OrderID, 
                        ProductID = cartItem.Product.ProductID,
                        Quantity = cartItem.Quantity
                    };

                    _detailOrderRepository.CreateDetailOrder(detailOrder);
                }

                HttpContext.Session.Remove("cart");
                HttpContext.Session.SetInt32("count", 0);

                url = "/Payment/CODSuccess";
            }


            return Json(new { paymentUrl = url });
        }
        public string CreatePaymentUrl(PaymentInformationModel model)
        {
            var url = _vnPayRepository.CreatePaymentUrl(model, HttpContext);
            return url.ToString();
        }

        public async Task<IActionResult> PaymentCallback()
        {
            var response = _vnPayRepository.PaymentExecute(Request.Query);
            ViewBag.PaymentResponse = response;
            string orderInfo = response.OrderDescription;
            string[] parts = orderInfo.Split('|');
            string deliveryMethod = parts[0]; 
            string shipAddress = parts[1];
            string note = parts[3];
            string nameDescriptionAmount = parts[4]; 
            string[] nameDescAmountParts = nameDescriptionAmount.Split(' ');
            string amount = nameDescAmountParts[^1];
            decimal totalDecimal = decimal.Parse(amount);


            if (response.Success)
            {
                string cartJson = HttpContext.Session.GetString("cart");
                List<CartModel> cart = string.IsNullOrEmpty(cartJson)
                    ? new List<CartModel>()
                    : JsonConvert.DeserializeObject<List<CartModel>>(cartJson);

                string serializedAccount = HttpContext.Session.GetString("LoggedInAccount");
                Account account = JsonConvert.DeserializeObject<Account>(serializedAccount);

                Customer customer = await _customerRepository.GetCustomerByAccountIdAsync(account.AccountID);

                Order order = new Order
                {
                    CustomerID = customer.CustomerID,
                    DeliveryMethod = deliveryMethod, 
                    CreateDate = DateTime.Now,
                    PayMethod = "VNPAY", 
                    PayStatus = "Success",
                    ShipAddress = shipAddress,
                    ShipStatus = "Pending",
                    Notes = note != "" ? note : null,
                    TotalAmount = (decimal?)totalDecimal
                };

                Order createdOrder = _orderRepository.CreateOrder(order);
                foreach (var cartItem in cart)
                {
                    DetailOrder detailOrder = new DetailOrder
                    {
                        OrderID = createdOrder.OrderID,
                        ProductID = cartItem.Product.ProductID,
                        Quantity = cartItem.Quantity
                    };

                    _detailOrderRepository.CreateDetailOrder(detailOrder);
                }
                ViewBag.OrderID = order.OrderID;

                HttpContext.Session.Remove("cart");
                HttpContext.Session.SetInt32("count", 0);

            }

            return View();
        }

        public IActionResult CODSuccess()
        {
           
            return View();
        }
    }
}