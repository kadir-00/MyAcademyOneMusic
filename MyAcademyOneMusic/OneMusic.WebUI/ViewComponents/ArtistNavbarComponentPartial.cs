using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OneMusic.EntityLayer.Entities;

namespace OneMusic.WebUI.ViewComponents
{
    public class ArtistNavbarComponentPartial : ViewComponent
    {
        private readonly UserManager<AppUser> _userManager;

        public ArtistNavbarComponentPartial(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            if (User.Identity?.Name != null)
            {
                var user = await _userManager.FindByNameAsync(User.Identity.Name);
                if (user != null)
                {
                    ViewBag.ArtistUserName = user.Name + " " + user.Surname;
                    ViewBag.ArtistImageUrl = user.ImageURL;
                    return View();
                }
            }

            ViewBag.ArtistUserName = "Misafir Sanatçı";
            ViewBag.ArtistImageUrl = "/one-music-gh-pages/img/bg-img/a1.jpg";
            return View();
        }
    }
}
