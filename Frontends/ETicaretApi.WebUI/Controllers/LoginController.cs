using ETicaretApi.Dto.Dtos.UserLoginDtos;
using Microsoft.AspNetCore.Mvc;

namespace ETicaretApi.WebUI.Controllers
{
    public class LoginController : Controller
    {
        [HttpGet]
        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SignIn(UserLoginDto userLoginDto)
        {
            return RedirectToAction("ProductList", "Product");
        }
    }
}
