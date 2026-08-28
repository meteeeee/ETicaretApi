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
    public class AdminUserController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AdminUserController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> UserList(int page = 1, int pageSize = 10)
        {
            ViewBag.v1 = "Kullanıcı & Yönetici Listesi";
            ViewBag.v2 = "Ana Sayfa";
            ViewBag.v3 = "Kullanıcı Yönetimi";

            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7035/api/Users");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var allUsers = JsonConvert.DeserializeObject<List<AdminResultUserDto>>(jsonData) ?? new List<AdminResultUserDto>();

                var totalItems = allUsers.Count;
                var totalPages = (int)Math.Ceiling((double)totalItems / pageSize);
                if (totalPages < 1) totalPages = 1;
                if (page < 1) page = 1;
                if (page > totalPages) page = totalPages;

                var pagedUsers = allUsers.Skip((page - 1) * pageSize).Take(pageSize).ToList();

                ViewBag.CurrentPage = page;
                ViewBag.TotalPages = totalPages;
                ViewBag.TotalItems = totalItems;
                ViewBag.PageSize = pageSize;

                return View(pagedUsers);
            }

            ViewBag.CurrentPage = 1;
            ViewBag.TotalPages = 1;
            ViewBag.TotalItems = 0;
            ViewBag.PageSize = pageSize;
            return View(new List<AdminResultUserDto>());
        }

        [HttpGet]
        public IActionResult CreateAdmin()
        {
            ViewBag.v1 = "Yeni Yönetici (Admin) Ekle";
            ViewBag.v2 = "Kullanıcılar";
            ViewBag.v3 = "Yeni Admin";
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateAdmin(AdminCreateAdminDto dto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(dto);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var responseMessage = await client.PostAsync("https://localhost:7035/api/Users/CreateAdmin", content);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("UserList");
            }

            var error = await responseMessage.Content.ReadAsStringAsync();
            ViewBag.Error = error;
            return View(dto);
        }

        [HttpGet]
        public async Task<IActionResult> UpdateUser(Guid id)
        {
            ViewBag.v1 = "Kullanıcı / Admin Düzenle";
            ViewBag.v2 = "Kullanıcılar";
            ViewBag.v3 = "Düzenle";

            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"https://localhost:7035/api/Users/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var value = JsonConvert.DeserializeObject<AdminUpdateUserDto>(jsonData);
                return View(value);
            }
            return RedirectToAction("UserList");
        }

        [HttpPost]
        public async Task<IActionResult> UpdateUser(AdminUpdateUserDto dto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(dto);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var responseMessage = await client.PutAsync("https://localhost:7035/api/Users/UpdateUser", content);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("UserList");
            }

            var error = await responseMessage.Content.ReadAsStringAsync();
            ViewBag.Error = error;
            return View(dto);
        }

        public async Task<IActionResult> DeleteUser(Guid id)
        {
            var client = _httpClientFactory.CreateClient();
            await client.DeleteAsync($"https://localhost:7035/api/Users/{id}");
            return RedirectToAction("UserList");
        }
    }
}
