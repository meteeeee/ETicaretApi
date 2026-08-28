using Microsoft.AspNetCore.Mvc;

namespace ETicaretApi.WebUI.Controllers
{
    public class AdminLayoutController : Controller
    {
        public IActionResult Index()
        {
            return RedirectToAction("ProductList", "AdminProduct", new { area = "Admin" });
        }
    }
}
