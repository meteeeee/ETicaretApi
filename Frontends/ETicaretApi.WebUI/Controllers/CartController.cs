using ETicaretApi.Dto.Dtos.AdminProductDtos;
using ETicaretApi.Dto.Dtos.CartDtos;
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
    public class CartController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private const string CartSessionKey = "UserCartSession";

        public CartController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        private CartDto GetCart()
        {
            var sessionData = HttpContext.Session.GetString(CartSessionKey);
            if (string.IsNullOrEmpty(sessionData))
            {
                return new CartDto();
            }
            return JsonConvert.DeserializeObject<CartDto>(sessionData) ?? new CartDto();
        }

        private void SaveCart(CartDto cart)
        {
            var json = JsonConvert.SerializeObject(cart);
            HttpContext.Session.SetString(CartSessionKey, json);
        }

        [HttpGet]
        public IActionResult Index()
        {
            var cart = GetCart();
            return View(cart);
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart(Guid productId, int quantity = 1)
        {
            if (quantity <= 0) quantity = 1;

            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync($"https://localhost:7035/api/Products/GetProduct?id={productId}");

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var product = JsonConvert.DeserializeObject<AdminResultProductDto>(json);

                if (product != null)
                {
                    var cart = GetCart();
                    var existingItem = cart.Items.FirstOrDefault(x => x.ProductID == productId);

                    if (existingItem != null)
                    {
                        existingItem.Quantity += quantity;
                    }
                    else
                    {
                        cart.Items.Add(new CartItemDto
                        {
                            ProductID = product.ProductID,
                            ProductName = product.ProductName,
                            ProductImageURL = product.ProductImageURL,
                            Price = product.ProductPrice,
                            Quantity = quantity
                        });
                    }

                    SaveCart(cart);
                    TempData["CartSuccess"] = $"{product.ProductName} ({quantity} adet) başarıyla sepete eklendi!";
                }
            }

            // Sayfada kalması için önceki sayfaya dön (Sepete otomatik yönlendirme yapma)
            var referer = Request.Headers["Referer"].ToString();
            if (!string.IsNullOrEmpty(referer))
            {
                return Redirect(referer);
            }

            return RedirectToAction("ProductDetail", "Product", new { id = productId });
        }

        [HttpGet]
        public IActionResult RemoveFromCart(Guid productId)
        {
            var cart = GetCart();
            var item = cart.Items.FirstOrDefault(x => x.ProductID == productId);
            if (item != null)
            {
                cart.Items.Remove(item);
                SaveCart(cart);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult UpdateQuantity(Guid productId, int quantity)
        {
            var cart = GetCart();
            var item = cart.Items.FirstOrDefault(x => x.ProductID == productId);
            if (item != null)
            {
                if (quantity <= 0)
                {
                    cart.Items.Remove(item);
                }
                else
                {
                    item.Quantity = quantity;
                }
                SaveCart(cart);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Checkout()
        {
            if (User.Identity == null || !User.Identity.IsAuthenticated)
            {
                return RedirectToAction("SignIn", "Login");
            }

            var cart = GetCart();
            if (cart.Items == null || !cart.Items.Any())
            {
                return RedirectToAction("Index");
            }

            var userIdStr = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var userId = Guid.TryParse(userIdStr, out var parsedGuid) ? parsedGuid : Guid.Parse("11111111-2222-3333-4444-555555555555");

            var client = _httpClientFactory.CreateClient();

            // 1. Order (Sipariş) Oluştur
            var orderCommand = new
            {
                UserID = userId,
                OrderDate = DateTime.Now,
                TotalPrice = cart.GrandTotal,
                OrderStatus = "Hazırlanıyor"
            };

            var orderJson = JsonConvert.SerializeObject(orderCommand);
            var orderContent = new StringContent(orderJson, Encoding.UTF8, "application/json");

            var orderResponse = await client.PostAsync("https://localhost:7035/api/Orders", orderContent);
            if (orderResponse.IsSuccessStatusCode)
            {
                var responseContent = await orderResponse.Content.ReadAsStringAsync();
                dynamic resultObj = JsonConvert.DeserializeObject(responseContent);
                string orderIdStr = resultObj?.orderId != null ? (string)resultObj.orderId : Guid.NewGuid().ToString();
                Guid orderId = Guid.Parse(orderIdStr);

                // 2. Her bir sepet ürünü için OrderDetail oluştur
                foreach (var item in cart.Items)
                {
                    var detailCommand = new
                    {
                        OrderID = orderId,
                        ProductID = item.ProductID,
                        Quantity = item.Quantity,
                        UnitPrice = item.Price
                    };

                    var detailJson = JsonConvert.SerializeObject(detailCommand);
                    var detailContent = new StringContent(detailJson, Encoding.UTF8, "application/json");
                    await client.PostAsync("https://localhost:7035/api/OrderDetails", detailContent);
                }

                // 3. Sepeti temizle
                HttpContext.Session.Remove(CartSessionKey);

                return RedirectToAction("OrderSuccess", new { orderId = orderId });
            }

            TempData["CheckoutError"] = "Sipariş oluşturulurken bir hata meydana geldi.";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult OrderSuccess(Guid orderId)
        {
            ViewBag.OrderId = orderId;
            return View();
        }
    }
}
