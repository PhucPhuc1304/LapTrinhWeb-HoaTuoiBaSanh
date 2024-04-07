using Microsoft.AspNetCore.Mvc;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace CF_HOATUOIBASANH.Controllers
{
    public class AddressController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly string _baseUrl = "https://vietnamese-administration.vercel.app/";

        public AddressController(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<IActionResult> GetAllCities()
        {
            var client = _clientFactory.CreateClient();
            var response = await client.GetAsync($"{_baseUrl}city");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return Content(content, "application/json");
            }
            else
            {
                return StatusCode((int)response.StatusCode);
            }
        }
        public async Task<IActionResult> GetDistrictsByCityId(string cityId)
        {
            var client = _clientFactory.CreateClient();
            var response = await client.GetAsync($"{_baseUrl}district?cityId={cityId}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return Content(content, "application/json");
            }
            else
            {
                return StatusCode((int)response.StatusCode);
            }
        }

        public async Task<IActionResult> GetWardsByDistrictId(string districtId)
        {
            var client = _clientFactory.CreateClient();
            var response = await client.GetAsync($"{_baseUrl}ward?districtId={districtId}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return Content(content, "application/json");
            }
            else
            {
                return StatusCode((int)response.StatusCode);
            }
        }
    }
}
