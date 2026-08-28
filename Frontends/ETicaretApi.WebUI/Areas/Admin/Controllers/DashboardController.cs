using Microsoft.AspNetCore.Mvc;

namespace ETicaretApi.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.v1 = "Genel Bakış";
            ViewBag.v2 = "Admin";
            ViewBag.v3 = "Dashboard";
            return View();
        }
    }
}
