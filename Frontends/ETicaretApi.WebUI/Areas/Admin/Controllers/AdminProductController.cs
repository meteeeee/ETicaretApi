using ETicaretApi.Dto.Dtos.AdminCategoryDtos;
using ETicaretApi.Dto.Dtos.AdminProductDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Text;

namespace ETicaretApi.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AdminProductController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AdminProductController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> ProductList()
        {
            ViewBag.v1 = "Ürün Listesi";
            ViewBag.v2 = "Ana Sayfa";
            ViewBag.v3 = "Tüm Ürünler";

            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7035/api/Products/ProductListWithCategory");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<AdminResultProductDto>>(jsonData);
                return View(values);
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> CreateProduct()
        {
            ViewBag.v1 = "Yeni Ürün Girişi";
            ViewBag.v2 = "Ürünler";
            ViewBag.v3 = "Yeni Ürün Ekle";

            var client = _httpClientFactory.CreateClient();
            var categoryResponse = await client.GetAsync("https://localhost:7035/api/Categories");
            if (categoryResponse.IsSuccessStatusCode)
            {
                var categoryJson = await categoryResponse.Content.ReadAsStringAsync();
                var categories = JsonConvert.DeserializeObject<List<AdminResultCategoryDto>>(categoryJson);
                ViewBag.CategoryList = categories?.Select(x => new SelectListItem
                {
                    Text = x.CategoryName,
                    Value = x.CategoryID.ToString()
                }).ToList();
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(AdminCreateProductDto adminCreateProductDto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(adminCreateProductDto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync("https://localhost:7035/api/Products", stringContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("ProductList", "AdminProduct", new { area = "Admin" });
            }
            return View();
        }

        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.DeleteAsync("https://localhost:7035/api/Products?id=" + id);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("ProductList", "AdminProduct", new { area = "Admin" });
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> UpdateProduct(Guid id)
        {
            ViewBag.v1 = "Ürün Güncelleme";
            ViewBag.v2 = "Ürünler";
            ViewBag.v3 = "Ürün Düzenle";

            var client = _httpClientFactory.CreateClient();
            
            // Kategori Dropdown'ı için
            var categoryResponse = await client.GetAsync("https://localhost:7035/api/Categories");
            if (categoryResponse.IsSuccessStatusCode)
            {
                var categoryJson = await categoryResponse.Content.ReadAsStringAsync();
                var categories = JsonConvert.DeserializeObject<List<AdminResultCategoryDto>>(categoryJson);
                ViewBag.CategoryList = categories?.Select(x => new SelectListItem
                {
                    Text = x.CategoryName,
                    Value = x.CategoryID.ToString()
                }).ToList();
            }

            // Mevcut Ürün Bilgisi
            var responseMessage = await client.GetAsync("https://localhost:7035/api/Products/GetProduct?id=" + id);
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var value = JsonConvert.DeserializeObject<AdminUpdateProductDto>(jsonData);
                return View(value);
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProduct(AdminUpdateProductDto adminUpdateProductDto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(adminUpdateProductDto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PutAsync("https://localhost:7035/api/Products", stringContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("ProductList", "AdminProduct", new { area = "Admin" });
            }
            return View();
        }
    }
}
