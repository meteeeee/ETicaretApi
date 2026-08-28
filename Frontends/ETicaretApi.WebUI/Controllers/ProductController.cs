using ETicaretApi.Dto.Dtos.ProductDtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretApi.WebUI.Controllers
{
    public class ProductController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ProductController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> ProductList(Guid? categoryId)
        {
            ViewBag.v1 = "Ürün Listesi";
            ViewBag.v2 = "Ana Sayfa";
            ViewBag.v3 = "Tüm Ürünler";
            ViewBag.selectedCategory = categoryId;

            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7035/api/Products");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultProductDto>>(jsonData);
                
                if (categoryId.HasValue && categoryId != Guid.Empty)
                {
                    values = values?.Where(x => x.ProductCategoryID == categoryId.Value).ToList();
                }

                return View(values);
            }

            return View();
        }

        public async Task<IActionResult> ProductDetail(Guid id)
        {
            ViewBag.id = id;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddReview(Guid productId, string comment, int rating)
        {
            var currentUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(currentUserId) || currentUserId == "00000000-0000-0000-0000-000000000000")
            {
                currentUserId = "11111111-2222-3333-4444-555555555555";
            }

            var reviewDto = new
            {
                ProductID = productId,
                UserID = Guid.Parse(currentUserId),
                Comment = comment,
                Rating = rating,
                ReviewDate = DateTime.Now
            };

            var client = _httpClientFactory.CreateClient();
            var json = JsonConvert.SerializeObject(reviewDto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            await client.PostAsync("https://localhost:7035/api/Reviews", content);
            return RedirectToAction("ProductDetail", new { id = productId });
        }
    }
}
