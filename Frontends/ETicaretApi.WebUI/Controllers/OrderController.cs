using ETicaretApi.Dto.Dtos.AdminOrderDtos;
using ETicaretApi.Dto.Dtos.AdminProductDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ETicaretApi.WebUI.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public OrderController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public async Task<IActionResult> MyOrders()
        {
            ViewBag.v1 = "Siparişlerim";
            ViewBag.v2 = "Ana Sayfa";
            ViewBag.v3 = "Geçmiş Siparişlerim";

            var userIdStr = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!Guid.TryParse(userIdStr, out var currentUserId))
            {
                return RedirectToAction("SignIn", "Login");
            }

            var client = _httpClientFactory.CreateClient();
            var ordersResponse = await client.GetAsync("https://localhost:7035/api/Orders");
            var detailsResponse = await client.GetAsync("https://localhost:7035/api/OrderDetails");
            var productsResponse = await client.GetAsync("https://localhost:7035/api/Products");

            if (ordersResponse.IsSuccessStatusCode)
            {
                var ordersJson = await ordersResponse.Content.ReadAsStringAsync();
                var allOrders = JsonConvert.DeserializeObject<List<AdminResultOrderDto>>(ordersJson) ?? new List<AdminResultOrderDto>();

                // Sadece giriş yapan kullanıcıya ait siparişler
                var userOrders = allOrders.Where(x => x.UserID == currentUserId).OrderByDescending(x => x.OrderDate).ToList();

                List<AdminResultOrderItemDto> allDetails = new List<AdminResultOrderItemDto>();
                if (detailsResponse.IsSuccessStatusCode)
                {
                    var detailsJson = await detailsResponse.Content.ReadAsStringAsync();
                    allDetails = JsonConvert.DeserializeObject<List<AdminResultOrderItemDto>>(detailsJson) ?? new List<AdminResultOrderItemDto>();
                }

                List<AdminResultProductDto> allProducts = new List<AdminResultProductDto>();
                if (productsResponse.IsSuccessStatusCode)
                {
                    var productsJson = await productsResponse.Content.ReadAsStringAsync();
                    allProducts = JsonConvert.DeserializeObject<List<AdminResultProductDto>>(productsJson) ?? new List<AdminResultProductDto>();
                }

                // Sipariş ürün detaylarını eşleştir
                foreach (var order in userOrders)
                {
                    var orderItems = allDetails.Where(d => d.OrderID == order.OrderID).ToList();
                    foreach (var item in orderItems)
                    {
                        var prod = allProducts.FirstOrDefault(p => p.ProductID == item.ProductID);
                        if (prod != null)
                        {
                            item.ProductName = prod.ProductName;
                            item.ProductImageURL = prod.ProductImageURL;
                        }
                    }
                    order.OrderDetails = orderItems;
                }

                return View(userOrders);
            }

            return View(new List<AdminResultOrderDto>());
        }
    }
}
