using Microsoft.AspNetCore.Mvc;

namespace OneMusic.WebUI.ViewComponents
{
    public class _AdminLayoutSideBarComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
