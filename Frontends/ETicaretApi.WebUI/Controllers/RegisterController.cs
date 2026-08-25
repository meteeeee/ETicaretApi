using ETicaretApi.Dto.Dtos.UserRegisterDtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

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
                TempData["RegisterSuccess"] = "Hesabınız başarıyla oluşturuldu! Şimdi giriş yapabilirsiniz.";
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
