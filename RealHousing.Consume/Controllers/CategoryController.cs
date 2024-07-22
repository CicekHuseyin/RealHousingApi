using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RealHousing.Consume.Models;
using System.Text.Json.Serialization;

namespace RealHousing.Consume.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public CategoryController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:44352/api/Category");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values= JsonConvert.DeserializeObject<List<CategoryListViewModel>>(jsonData);
                return Ok(values);
            }
            return View();
        }
    }
}
