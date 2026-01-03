using Microsoft.AspNetCore.Mvc;

namespace OneMusic.WebUI.Areas.Default.Controllers
{
    [Area("Default")]
    public class _DefaultLayout : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
