using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OneMusic.BusinessLayer.Abstract;
using OneMusic.EntityLayer.Entities;

namespace OneMusic.WebUI.Areas.Artist.Controllers
{
    [Area("Artist")]
    [Authorize(Roles = "Artist")]
    [Route("[area]/[controller]/[action]/{id?}")]
    public class AlbumStatusController : Controller
    {
        private readonly IAlbumService albumService;
        private readonly UserManager<AppUser> userManager;

        public AlbumStatusController(IAlbumService albumService, UserManager<AppUser> userManager)
        {
            this.albumService = albumService;
            this.userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            AppUser user = null;
            if (User.Identity?.Name != null)
            {
                user = await userManager.FindByNameAsync(User.Identity.Name);
            }

            if (user == null)
            {
                user = userManager.Users.FirstOrDefault();
            }

            if (user != null)
            {
                var list = albumService.TgetListAwatingApprovalAlbums(user.Id);
                return View(list);
            }

            // Fallback for no user at all
            return View(new List<Album>());
        }
    }
}
