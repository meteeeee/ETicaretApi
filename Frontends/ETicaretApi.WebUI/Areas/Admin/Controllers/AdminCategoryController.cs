using ETicaretApi.Dto.Dtos.AdminCategoryDtos;
using ETicaretApi.Dto.Dtos.AdminProductDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace ETicaretApi.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AdminCategoryController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AdminCategoryController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> CategoryList()
        {
            ViewBag.v1 = "Kategori Listesi";
            ViewBag.v2 = "Ana Sayfa";
            ViewBag.v3 = "Tüm Kategoriler";

            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7035/api/Categories");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<AdminResultCategoryDto>>(jsonData);
                return View(values);
            }
            return View();
        }

        [HttpGet]
        public IActionResult CreateCategory()
        {
            ViewBag.v1 = "Yeni Kategori Girişi";
            ViewBag.v2 = "Kategoriler";
            ViewBag.v3 = "Yeni Kategori Ekle";
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory(AdminCreateCategoryDto adminCreateCategoryDto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(adminCreateCategoryDto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync("https://localhost:7035/api/Categories", stringContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("CategoryList", "AdminCategory", new { area = "Admin" });
            }
            return View();
        }

        public async Task<IActionResult> DeleteCategory(Guid id)
        {
            var client = _httpClientFactory.CreateClient();

            // Kategoriye ait ürün var mı kontrolü
            var productResponse = await client.GetAsync("https://localhost:7035/api/Products");
            if (productResponse.IsSuccessStatusCode)
            {
                var productJson = await productResponse.Content.ReadAsStringAsync();
                var products = JsonConvert.DeserializeObject<List<AdminResultProductDto>>(productJson);
                var relatedProductsCount = products?.Count(x => x.ProductCategoryID == id) ?? 0;

                if (relatedProductsCount > 0)
                {
                    TempData["CategoryDeleteError"] = $"Bu kategoriye bağlı {relatedProductsCount} adet ürün bulunmaktadır! Kategoriyi silebilmek için önce altındaki ürünleri siliniz veya başka bir kategoriye taşıyınız.";
                    return RedirectToAction("CategoryList", "AdminCategory", new { area = "Admin" });
                }
            }

            var responseMessage = await client.DeleteAsync("https://localhost:7035/api/Categories?id=" + id);
            if (responseMessage.IsSuccessStatusCode)
            {
                TempData["CategoryDeleteSuccess"] = "Kategori başarıyla silindi.";
                return RedirectToAction("CategoryList", "AdminCategory", new { area = "Admin" });
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> UpdateCategory(Guid id)
        {
            ViewBag.v1 = "Kategori Güncelleme";
            ViewBag.v2 = "Kategoriler";
            ViewBag.v3 = "Kategori Düzenle";

            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7035/api/Categories/GetCategory?id=" + id);
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var value = JsonConvert.DeserializeObject<AdminUpdateCategoryDto>(jsonData);
                return View(value);
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateCategory(AdminUpdateCategoryDto adminUpdateCategoryDto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(adminUpdateCategoryDto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PutAsync("https://localhost:7035/api/Categories", stringContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("CategoryList", "AdminCategory", new { area = "Admin" });
            }
            return View();
        }
    }
}
