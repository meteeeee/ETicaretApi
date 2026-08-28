using ETicaretApi.Dto.Dtos.UserRegisterDtos;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretApi.WebUI.Controllers
{
    public class RegisterController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public RegisterController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(CreateUserRegisterDto createUserRegisterDto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(createUserRegisterDto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync("https://localhost:7035/api/Registers", stringContent);

            if (responseMessage.IsSuccessStatusCode)
            {
                // Kayıt olur olmaz otomatik giriş yap
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, createUserRegisterDto.UserName),
                    new Claim(ClaimTypes.Role, "User"),
                    new Claim(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString())
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    new AuthenticationProperties { IsPersistent = false });

                TempData["RegisterSuccess"] = $"Hoş geldiniz, {createUserRegisterDto.FirstName}! Hesabınız oluşturuldu ve otomatik giriş yapıldı.";
                return RedirectToAction("ProductList", "Product");
            }

            // Hata durumunda Ad, Soyad, Cinsiyet ve Adresi koru, sadece çakışan bilgileri sıfırla
            TempData["RegisterError"] = "Bu kullanıcı adı veya e-posta adresi zaten kullanımda!";
            TempData["FirstName"] = createUserRegisterDto.FirstName;
            TempData["LastName"] = createUserRegisterDto.LastName;
            TempData["Gender"] = createUserRegisterDto.Gender;
            TempData["Address"] = createUserRegisterDto.Address;
            TempData["Password"] = createUserRegisterDto.Password;

            var referer = Request.Headers["Referer"].ToString();
            return !string.IsNullOrEmpty(referer) ? Redirect(referer) : RedirectToAction("Index", "Home");
        }
    }
}
