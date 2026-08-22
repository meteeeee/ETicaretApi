using Microsoft.AspNetCore.Mvc;

namespace ETicaretApi.WebUI.Controllers
{
    public class UserWebUILayoutController : Controller
    {
        public IActionResult LayoutUI()
        {
            return View();
        }
    }
}
