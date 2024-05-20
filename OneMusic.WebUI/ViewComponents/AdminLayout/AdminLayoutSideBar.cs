using Microsoft.AspNetCore.Mvc;

namespace OneMusic.WebUI.ViewComponents.AdminLayout
{
    public class AdminLayoutSideBar : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();

        }
    }
}
