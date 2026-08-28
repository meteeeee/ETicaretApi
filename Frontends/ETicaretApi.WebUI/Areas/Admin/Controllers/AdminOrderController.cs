using ETicaretApi.Dto.Dtos.AdminOrderDtos;
using ETicaretApi.Dto.Dtos.AdminProductDtos;
using ETicaretApi.Dto.Dtos.AdminUserDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretApi.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AdminOrderController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AdminOrderController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> OrderList()
        {
            ViewBag.v1 = "Sipariş Listesi";
            ViewBag.v2 = "Ana Sayfa";
            ViewBag.v3 = "Sipariş Yönetimi";

            var client = _httpClientFactory.CreateClient();
            var ordersResponse = await client.GetAsync("https://localhost:7035/api/Orders");
            var detailsResponse = await client.GetAsync("https://localhost:7035/api/OrderDetails");
            var productsResponse = await client.GetAsync("https://localhost:7035/api/Products");
            var usersResponse = await client.GetAsync("https://localhost:7035/api/Users");

            if (ordersResponse.IsSuccessStatusCode)
            {
                var ordersJson = await ordersResponse.Content.ReadAsStringAsync();
                var orders = JsonConvert.DeserializeObject<List<AdminResultOrderDto>>(ordersJson) ?? new List<AdminResultOrderDto>();

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

                List<AdminResultUserDto> allUsers = new List<AdminResultUserDto>();
                if (usersResponse.IsSuccessStatusCode)
                {
                    var usersJson = await usersResponse.Content.ReadAsStringAsync();
                    allUsers = JsonConvert.DeserializeObject<List<AdminResultUserDto>>(usersJson) ?? new List<AdminResultUserDto>();
                }

                // Siparişleri eşleştir
                foreach (var order in orders)
                {
                    var user = allUsers.FirstOrDefault(u => u.Id == order.UserID);
                    order.UserName = user != null ? user.UserName : "Bilinmeyen Müşteri";

                    // O siparişe ait ürün detayları
                    var orderItems = allDetails.Where(d => d.OrderID == order.OrderID).ToList();
                    foreach (var item in orderItems)
                    {
                        var prod = allProducts.FirstOrDefault(p => p.ProductID == item.ProductID);
                        if (prod != null)
                        {
                            item.ProductName = prod.ProductName;
                            item.ProductImageURL = prod.ProductImageURL;
                        }
                        else
                        {
                            item.ProductName = "Ürün Bilgisi Bulunamadı";
                        }
                    }
                    order.OrderDetails = orderItems;
                }

                return View(orders.OrderByDescending(x => x.OrderDate).ToList());
            }

            return View(new List<AdminResultOrderDto>());
        }

        [HttpPost]
        public async Task<IActionResult> UpdateStatus(Guid orderId, string status)
        {
            var client = _httpClientFactory.CreateClient();
            var getResponse = await client.GetAsync($"https://localhost:7035/api/Orders/{orderId}");
            if (getResponse.IsSuccessStatusCode)
            {
                var json = await getResponse.Content.ReadAsStringAsync();
                var order = JsonConvert.DeserializeObject<AdminResultOrderDto>(json);

                if (order != null)
                {
                    var updateCommand = new
                    {
                        OrderID = order.OrderID,
                        UserID = order.UserID,
                        OrderDate = order.OrderDate,
                        TotalPrice = order.TotalPrice,
                        OrderStatus = status
                    };

                    var updateJson = JsonConvert.SerializeObject(updateCommand);
                    var content = new StringContent(updateJson, Encoding.UTF8, "application/json");
                    await client.PutAsync("https://localhost:7035/api/Orders", content);
                }
            }

            return RedirectToAction("OrderList");
        }

        public async Task<IActionResult> DeleteOrder(Guid id)
        {
            var client = _httpClientFactory.CreateClient();
            await client.DeleteAsync($"https://localhost:7035/api/Orders/{id}");
            return RedirectToAction("OrderList");
        }
    }
}
