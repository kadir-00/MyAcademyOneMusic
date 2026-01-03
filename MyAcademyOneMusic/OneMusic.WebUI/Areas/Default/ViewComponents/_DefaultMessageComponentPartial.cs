using Microsoft.AspNetCore.Mvc;

namespace OneMusic.WebUI.Areas.Default.ViewComponents
{
    public class _DefaultMessageComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
