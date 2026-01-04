using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OneMusic.EntityLayer.Entities;

using Microsoft.AspNetCore.Authorization;

namespace OneMusic.WebUI.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminLayoutController : Controller
    {


        public IActionResult Index()
        {
            return View();
        }

        public PartialViewResult NotificationPartial()
        {
            return PartialView();
        }
    }
}
