using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace CF_HOATUOIBASANH.Controllers
{
    public class ShipController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _httpClientFactory;

        public ShipController(IConfiguration configuration, IHttpClientFactory httpClientFactory)
        {
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public async Task<string> GetToken()
        {
            try
            {
                // Lấy API_KEY_STG từ cấu hình
                string apiKey = _configuration.GetValue<string>("AhamoveApiSettings:ApiKey");

                // BASE NAME và số điện thoại mặc định
                string baseName = "Phuc";
                string baseMobile = "0964995622";

                // Tạo URL với các tham số cần thiết
                var url = $"https://apistg.ahamove.com/v1/partner/register_account?mobile={baseMobile}&name={baseName}&api_key={apiKey}";

                // Gửi yêu cầu GET đến API của Ahamove
                using (var client = new HttpClient())
                {
                    var response = await client.GetAsync(url);
                    response.EnsureSuccessStatusCode();

                    // Đọc nội dung phản hồi
                    var responseBody = await response.Content.ReadAsStringAsync();

                    // Phân tích mã thông tin đăng nhập từ JSON
                    var responseObject = JsonConvert.DeserializeObject<JObject>(responseBody);
                    var token = responseObject.Value<string>("token");

                    // Trả về token
                    return token;
                }
            }
            catch (HttpRequestException ex)
            {
                // Xử lý lỗi khi gửi yêu cầu
                return $"Internal server error: {ex.Message}";
            }
        }

        [HttpPost]
        public async Task<IActionResult> ShipCost(string name, string phone, string address)
        {
            try
            {
                // Gọi hàm GetToken() để lấy token
                string token = await GetToken();

                // Tạo danh sách các điểm và thông tin khác cần thiết cho yêu cầu
                var pathData = new List<object> {
                    new {
                        address = "77 Hồ Thị Kỷ,Phường 15, Quận 10, Hồ Chí Minh, Việt Nam",
                        name = "Anh Phúc",
                        mobile = "0964995622",
                        remarks = "call me"
                    },
                    new {

                        address = address,
                        name = name,
                        mobile = phone,
                    }
                };

                // Chuyển danh sách thành chuỗi JSON
                var orderPath = JsonConvert.SerializeObject(pathData);

                // Tạo nội dung cho yêu cầu
                var content = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("token", token),
                    new KeyValuePair<string, string>("order_time", "0"),
                    new KeyValuePair<string, string>("path", orderPath),
                    new KeyValuePair<string, string>("service_id", "SGN-BIKE"),
                    new KeyValuePair<string, string>("requests", "[]")
                });

                // Gửi yêu cầu HTTP POST tới API của Ahamove
                var client = _httpClientFactory.CreateClient();
                var response = await client.PostAsync("https://apistg.ahamove.com/v1/order/estimated_fee", content);
                response.EnsureSuccessStatusCode();

                // Đọc nội dung phản hồi
                var responseBody = await response.Content.ReadAsStringAsync();
                var responseObject = JObject.Parse(responseBody);
                var totalPrice = responseObject["total_price"].Value<decimal>();
                return Ok(totalPrice);
            }
            catch (Exception ex)
            {
                // Xử lý lỗi
                Console.WriteLine($"An error occurred: {ex.Message}");
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
    }
}
