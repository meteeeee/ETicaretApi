using ETicaretApi.Dto.Dtos.UserLoginDtos;
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
    public class LoginController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public LoginController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(UserLoginDto userLoginDto)
        {
            if (string.IsNullOrWhiteSpace(userLoginDto.UserName) || string.IsNullOrWhiteSpace(userLoginDto.Password))
            {
                ViewBag.Error = "Kullanıcı adı ve şifre zorunludur.";
                return View(userLoginDto);
            }

            var client = _httpClientFactory.CreateClient();
            var json = JsonConvert.SerializeObject(userLoginDto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("https://localhost:7035/api/Logins", content);
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                dynamic userData = JsonConvert.DeserializeObject(responseContent);

                string role = userData?.role != null ? (string)userData.role : "User";
                string userName = userData?.userName != null ? (string)userData.userName : userLoginDto.UserName;
                string userId = userData?.userId != null ? (string)userData.userId : Guid.NewGuid().ToString();

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, userName),
                    new Claim(ClaimTypes.Role, role),
                    new Claim(ClaimTypes.NameIdentifier, userId)
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = false
                };

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties);

                if (role.Equals("Admin", StringComparison.OrdinalIgnoreCase))
                {
                    return RedirectToAction("ProductList", "AdminProduct", new { area = "Admin" });
                }

                return RedirectToAction("ProductList", "Product");
            }

            var errorMsg = await response.Content.ReadAsStringAsync();
            ViewBag.Error = string.IsNullOrWhiteSpace(errorMsg) ? "Kullanıcı adı veya şifre hatalı!" : errorMsg.Trim('"');
            return View(userLoginDto);
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("ProductList", "Product");
        }

        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
