using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OneMusic.EntityLayer.Entities;

namespace OneMusic.WebUI.Controllers
{
    public class AdminLayoutController : Controller
    {
    

        public async Task<IActionResult> Index()
        {
            return View();
        }

        public PartialViewResult NotificationPartial()
        {
            return PartialView();
        }
    }
}
