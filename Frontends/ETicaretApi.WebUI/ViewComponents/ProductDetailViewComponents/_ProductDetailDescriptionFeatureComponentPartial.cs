using Microsoft.AspNetCore.Mvc;

namespace ETicaretApi.WebUI.ViewComponents.ProductDetailViewComponents
{
    public class _ProductDetailDescriptionFeatureComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke(Guid id)
        {
            return View();
        }
    }
}
