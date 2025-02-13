﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using RealHousing.Consume.Models;
using RealHousing.DataAccessLayer.Concreate;
using System.Text;

namespace RealHousing.Consume.Controllers
{
    public class ProductController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ProductController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:44352/api/Product");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ProductListViewModel>>(jsonData);
                return View(values);
            }
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> AddProduct()
        {
            //Burada ki kodlar Ürün eklerken Kategori deki verilerin Dropdowna gelmesi için yazıldı.
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync("https://localhost:44352/api/Category");

            var jsonData = await response.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<CategoryListViewModel>>(jsonData);
            List<SelectListItem> category = (from x in values
                                             select new SelectListItem
                                             {
                                                 Text = x.CategoryName,
                                                 Value = x.CategoryID.ToString()
                                             }).ToList();
            ViewBag.category = category;
            return View();

        }
        [HttpPost]
        public async Task<IActionResult> AddProduct(AddProductViewModel p)
        {
            //İstek oluşturdum.
            var client = _httpClientFactory.CreateClient();
            //Gelen veriyi serialize ettim.
            var jsonData = JsonConvert.SerializeObject(p);
            //Gelen içerik de türkçe karakter de olsun. Dosyayı json dosyası olarak gönderiyorum.
            StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            //Gönderileceği adres burası.
            var response = await client.PostAsync("https://localhost:44352/api/Product", content);
            //İşlem başarılı olursa index e yönlendir.
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");

            }
            return View();
        }
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.DeleteAsync($"https://localhost:44352/api/Product/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> UpdateProduct(int id)
        {
            //Burada ki kodlar Ürün eklerken Kategori deki verilerin Dropdowna gelmesi için yazıldı.
            var client2 = _httpClientFactory.CreateClient();
            var response2 = await client2.GetAsync("https://localhost:44352/api/Category");

            var jsonData2 = await response2.Content.ReadAsStringAsync();
            var values2 = JsonConvert.DeserializeObject<List<CategoryListViewModel>>(jsonData2);
            List<SelectListItem> category = (from x in values2
                                             select new SelectListItem
                                             {
                                                 Text = x.CategoryName,
                                                 Value = x.CategoryID.ToString()
                                             }).ToList();
            ViewBag.v = category;


            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync($"https://localhost:44352/api/Product/{id}");
            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<UpdateProductViewModel>(jsonData);
                return View(values);
            }
            return View();

        }
        [HttpPost]
        public async Task<IActionResult> UpdateProduct(UpdateProductViewModel updateProductViewModel)
        {
            var client = _httpClientFactory.CreateClient();
            //String formatını Json a çeviriyor.
            var jsonData = JsonConvert.SerializeObject(updateProductViewModel);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PutAsync("https://localhost:44352/api/Product/", stringContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}
