using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OneMusic.EntityLayer.Entities;

namespace OneMusic.WebUI.ViewComponents
{
    public class _AdminNavbarComponentPartial : ViewComponent
    {
        private readonly UserManager<AppUser> _userManager;

        public _AdminNavbarComponentPartial(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            if (User.Identity?.Name != null)
            {
                var value = await _userManager.FindByNameAsync(User.Identity.Name);
                if (value != null)
                {
                    ViewBag.userName = value.Name + " " + value.Surname;
                    ViewBag.ImageURL = value.ImageURL;
                    return View();
                }
            }

            ViewBag.userName = "Misafir Kullanıcı";
            ViewBag.ImageURL = "/one-music-gh-pages/img/bg-img/a1.jpg"; // Default image or null
            return View();
        }
    }
}
