using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OneMusic.BusinessLayer.Abstract;
using OneMusic.EntityLayer.Entities;
namespace OneMusic.WebUI.Controllers
{
    //[Authorize]
    public class MusicCountController : Controller
    {
        private readonly ISongsListenDetailsService _songsListenDetailsService;
        private readonly UserManager<AppUser> _userManager;
        public MusicCountController(ISongsListenDetailsService songsListenDetailsService, UserManager<AppUser> userManager)
        {
            _songsListenDetailsService = songsListenDetailsService;
            _userManager = userManager;
        }

        public async Task<JsonResult> Index(int id)
        {
            var username = User.Identity?.Name;
            if (username == null)
            {
                return Json(id);
            }
            var user = await _userManager.FindByNameAsync(username);

            if (user == null)
            {
                return Json(id);
            }
            bool result = _songsListenDetailsService.TIsActive(user.Id, id);
            if (result)
            {
                _songsListenDetailsService.TCreate(new SongsListenDetails
                {
                    SongId = id,
                    AppUserId = user.Id,

                });
            }

            return Json(id);
        }
    }
}
