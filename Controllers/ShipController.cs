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
                string apiKey = _configuration.GetValue<string>("AhamoveApiSettings:ApiKey");

                // BASE NAME và số điện thoại mặc định
                string baseName = "Phuc";
                string baseMobile = "0964995622";

                var url = $"https://apistg.ahamove.com/v1/partner/register_account?mobile={baseMobile}&name={baseName}&api_key={apiKey}";

                using (var client = new HttpClient())
                {
                    var response = await client.GetAsync(url);
                    response.EnsureSuccessStatusCode();

                    var responseBody = await response.Content.ReadAsStringAsync();

                    var responseObject = JsonConvert.DeserializeObject<JObject>(responseBody);
                    var token = responseObject.Value<string>("token");

                    return token;
                }
            }
            catch (HttpRequestException ex)
            {
                return $"Internal server error: {ex.Message}";
            }
        }

        [HttpPost]
        public async Task<IActionResult> ShipCost(string name, string phone, string address)
        {
            try
            {
                string token = await GetToken();

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

                var orderPath = JsonConvert.SerializeObject(pathData);

                var content = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("token", token),
                    new KeyValuePair<string, string>("order_time", "0"),
                    new KeyValuePair<string, string>("path", orderPath),
                    new KeyValuePair<string, string>("service_id", "SGN-BIKE"),
                    new KeyValuePair<string, string>("requests", "[]")
                });

                var client = _httpClientFactory.CreateClient();
                var response = await client.PostAsync("https://apistg.ahamove.com/v1/order/estimated_fee", content);
                response.EnsureSuccessStatusCode();
                var responseBody = await response.Content.ReadAsStringAsync();
                var responseObject = JObject.Parse(responseBody);
                var totalPrice = responseObject["total_price"].Value<decimal>();
                return Ok(totalPrice);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
    }
}
