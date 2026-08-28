using ETicaretApi.Dto.Dtos.AdminProductDtos;
using ETicaretApi.Dto.Dtos.AdminReviewDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ETicaretApi.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AdminReviewController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AdminReviewController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> ReviewList(int page = 1, int pageSize = 10)
        {
            ViewBag.v1 = "Müşteri Yorumları";
            ViewBag.v2 = "Ana Sayfa";
            ViewBag.v3 = "Yorum Yönetimi";

            var client = _httpClientFactory.CreateClient();

            // Yorumları çek
            var reviewResponse = await client.GetAsync("https://localhost:7035/api/Reviews");
            // Ürünleri çek (Ürün adını eşleştirmek için)
            var productResponse = await client.GetAsync("https://localhost:7035/api/Products");

            if (reviewResponse.IsSuccessStatusCode)
            {
                var reviewJson = await reviewResponse.Content.ReadAsStringAsync();
                var reviewList = JsonConvert.DeserializeObject<List<AdminResultReviewDto>>(reviewJson) ?? new List<AdminResultReviewDto>();

                if (productResponse.IsSuccessStatusCode)
                {
                    var productJson = await productResponse.Content.ReadAsStringAsync();
                    var productList = JsonConvert.DeserializeObject<List<AdminResultProductDto>>(productJson) ?? new List<AdminResultProductDto>();

                    foreach (var review in reviewList)
                    {
                        var matchingProduct = productList.FirstOrDefault(p => p.ProductID == review.ProductID);
                        review.ProductName = matchingProduct != null ? matchingProduct.ProductName : "Ürün Silinmiş";
                    }
                }

                var orderedReviews = reviewList.OrderByDescending(x => x.ReviewDate).ToList();
                var totalItems = orderedReviews.Count;
                var totalPages = (int)Math.Ceiling((double)totalItems / pageSize);
                if (totalPages < 1) totalPages = 1;
                if (page < 1) page = 1;
                if (page > totalPages) page = totalPages;

                var pagedReviews = orderedReviews.Skip((page - 1) * pageSize).Take(pageSize).ToList();

                ViewBag.CurrentPage = page;
                ViewBag.TotalPages = totalPages;
                ViewBag.TotalItems = totalItems;
                ViewBag.PageSize = pageSize;

                return View(pagedReviews);
            }

            ViewBag.CurrentPage = 1;
            ViewBag.TotalPages = 1;
            ViewBag.TotalItems = 0;
            ViewBag.PageSize = pageSize;
            return View(new List<AdminResultReviewDto>());
        }

        public async Task<IActionResult> DeleteReview(Guid id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.DeleteAsync($"https://localhost:7035/api/Reviews/{id}");
            return RedirectToAction("ReviewList");
        }
    }
}
