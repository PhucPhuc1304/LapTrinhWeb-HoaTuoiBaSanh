using CF_HOATUOIBASANH.Interfaces;
using CF_HOATUOIBASANH.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;

namespace CF_HOATUOIBASANH.Controllers
{
    public class PaymentController : Controller
    {

        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IVNPayRepository _vnPayRepository;

        public PaymentController(IConfiguration configuration, IHttpClientFactory httpClientFactory, IVNPayRepository vnPayRepository)
        {
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
            _vnPayRepository = vnPayRepository;
        }

        public async Task<IActionResult> ProcessPayment(string lastName, string firstName, string phone, string email, string note, string result, string paymentMethod, string deliveryMethod, string typePayment, bool useShip, double totalPrice)
        {
            var total = totalPrice;
            if (useShip)
            {
                try
                {
                    // Create an instance of ShipController
                    var shipController = new ShipController(_configuration, _httpClientFactory);

                    // Call the ShipCost method from ShipController instance
                    var shipCostResponse = await shipController.ShipCost(firstName, phone, result);
                    var shipCostResponseObjectResult = shipCostResponse as OkObjectResult;
                    var ship_ = shipCostResponseObjectResult.Value;
                    double shippingPrice = Convert.ToDouble(ship_);
                    total += shippingPrice;

                }
                catch (Exception ex)
                {
                    // Handle the error
                    Console.WriteLine($"An error occurred: {ex.Message}");
                    return StatusCode(500, $"An error occurred: {ex.Message}");
                }
            }

            DateTime currentTime = DateTime.Now;

            // Tạo chuỗi string với thông điệp và thời gian hiện tại
            string message = $"Thanh toán cho đơn hàng {currentTime} của {lastName}";
            Console.WriteLine(total);
            PaymentInformationModel newPaymentInfo = new PaymentInformationModel
            {
                OrderType = "ONL",
                Amount = totalPrice,
                OrderDescription = message,
                Name = lastName
            };



            Console.WriteLine(newPaymentInfo);

            string url = CreatePaymentUrl(newPaymentInfo);

            return Json(new { paymentUrl = url });
        }
        public string CreatePaymentUrl(PaymentInformationModel model)
        {
            var url = _vnPayRepository.CreatePaymentUrl(model, HttpContext);
            return url.ToString();
        }

        public IActionResult PaymentCallback()
        {
           // var  response = _vnPayRepository.PaymentExecute(Request.Query);
            //ViewBag.PaymentResponse = response;

            return View();
        }
    }
}