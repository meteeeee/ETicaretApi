using ETicaretApi.Dto.Dtos.AdminCategoryDtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ETicaretApi.WebUI.ViewComponents.UserLayoutWebUIViewComponents
{
    public class _UserLayoutWebUICategoriesComponentPartial : ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public _UserLayoutWebUICategoriesComponentPartial(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync("https://localhost:7035/api/Categories");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var categories = JsonConvert.DeserializeObject<List<AdminResultCategoryDto>>(json);
                return View(categories);
            }
            return View();
        }
    }
}
