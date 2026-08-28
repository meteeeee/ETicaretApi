using ETicaretApi.Dto.Dtos.CartDtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Linq;

namespace ETicaretApi.WebUI.ViewComponents.UserLayoutWebUIViewComponents
{
    public class _UserLayoutWebUITopbarComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var sessionData = HttpContext.Session.GetString("UserCartSession");
            int count = 0;
            if (!string.IsNullOrEmpty(sessionData))
            {
                var cart = JsonConvert.DeserializeObject<CartDto>(sessionData);
                count = cart?.Items?.Sum(x => x.Quantity) ?? 0;
            }
            ViewBag.CartCount = count;
            return View();
        }
    }
}
