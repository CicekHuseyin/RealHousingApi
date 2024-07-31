using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RealHousing.Consume.Models;
using System.Text;
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
                var values = JsonConvert.DeserializeObject<List<CategoryListViewModel>>(jsonData);
                //return Ok(values);
                return View(values);
            }
            return View();
        }
        [HttpGet]
        public IActionResult AddCategory()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddCategory(AddCategoryViewModel addCategoryViewModel)
        {
            //İstek oluşturdum.
            var client = _httpClientFactory.CreateClient();
            //Gelen veriyi serialize ettim.
            var jsonData = JsonConvert.SerializeObject(addCategoryViewModel);
            //Gelen içerik de türkçe karakter de olsun. Dosyayı json dosyası olarak gönderiyorum.
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            //Gönderileceği adres burası.
            var responseMessage = await client.PostAsync("https://localhost:44352/api/Category", stringContent);
            //İşlem başarılı olursa index e yönlendir.
            if (responseMessage.IsSuccessStatusCode)
            {
                RedirectToAction("Index");
            }
            return View();
        }
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.DeleteAsync($"https://localhost:44352/api/Category/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> UpdateCategory(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"https://localhost:44352/api/Category/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                //Veriyi string olarak okuyor.
                var jsonData=await responseMessage.Content.ReadAsStringAsync();
                //Json formatını string e çeviriyor.
                var values=JsonConvert.DeserializeObject<UpdateCategoryViewModel>(jsonData);
                return View(values);
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> UpdateCategory(UpdateCategoryViewModel updateCategoryViewModel)
        {
            var client = _httpClientFactory.CreateClient();
            //String formatını Json a çeviriyor.
            var jsonData = JsonConvert.SerializeObject(updateCategoryViewModel);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PutAsync("https://localhost:44352/api/Category/",stringContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

    }
}
