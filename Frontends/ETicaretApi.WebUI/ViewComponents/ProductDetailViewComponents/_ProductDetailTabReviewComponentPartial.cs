using ETicaretApi.Dto.Dtos.AdminReviewDtos;
using ETicaretApi.Dto.Dtos.AdminUserDtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ETicaretApi.WebUI.ViewComponents.ProductDetailViewComponents
{
    public class _ProductDetailTabReviewComponentPartial : ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public _ProductDetailTabReviewComponentPartial(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync(Guid id)
        {
            ViewBag.ProductId = id;
            var client = _httpClientFactory.CreateClient();

            // Yorumları çek
            var response = await client.GetAsync($"https://localhost:7035/api/Reviews/GetReviewsByProductId/{id}");
            // Kullanıcıları çek (UserName eşleştirmek için)
            var usersResponse = await client.GetAsync("https://localhost:7035/api/Users");

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var list = JsonConvert.DeserializeObject<List<AdminResultReviewDto>>(json) ?? new List<AdminResultReviewDto>();

                if (usersResponse.IsSuccessStatusCode)
                {
                    var usersJson = await usersResponse.Content.ReadAsStringAsync();
                    var usersList = JsonConvert.DeserializeObject<List<AdminResultUserDto>>(usersJson);

                    if (usersList != null)
                    {
                        foreach (var review in list)
                        {
                            var user = usersList.FirstOrDefault(u => u.Id == review.UserID);
                            review.UserName = user != null ? user.UserName : "kullanici";
                        }
                    }
                }

                return View(list);
            }

            return View(new List<AdminResultReviewDto>());
        }
    }
}
