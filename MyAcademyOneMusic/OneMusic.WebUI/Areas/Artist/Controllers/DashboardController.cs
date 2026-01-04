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
    public class DashboardController : Controller
    {

        private readonly UserManager<AppUser> _userManager;
        private readonly IAlbumService _albumService;
        private readonly ISongService _songService;
        private readonly ISongsListenDetailsService _songsListenDetailsService;

        public DashboardController(IAlbumService albumService, UserManager<AppUser> userManager, ISongService songService, ISongsListenDetailsService songsListenDetailsService)
        {
            _albumService = albumService;
            _userManager = userManager;
            _songService = songService;
            _songsListenDetailsService = songsListenDetailsService;
        }

        public async Task<IActionResult> Index()
        {
            AppUser user = null;
            if (User.Identity?.Name != null)
            {
                user = await _userManager.FindByNameAsync(User.Identity.Name);
            }

            if (user == null)
            {
                // Fallback for no login: get the first user
                user = _userManager.Users.FirstOrDefault();
            }

            if (user != null)
            {
                ViewBag.albumCount = _albumService.TAlbumCount(user.Id);
                ViewBag.WaitingAlbumCount = _albumService.TAlbumCountByWaiting(user.Id);
                ViewBag.ExpensiveAlbumName = _albumService.TExpensiveAlbumName(user.Id);
                ViewBag.SongCount = _songService.TSongCount(user.Id);
                ViewBag.Email = user.Email;
                ViewBag.PhoneNumber = user.PhoneNumber;
                ViewBag.IsTwoFactor = user.TwoFactorEnabled;
                ViewBag.ListenCount = _songsListenDetailsService.TcountByListenedArtist(user.Id);
                ViewBag.ArtistUserName = user.Name + " " + user.Surname;
                ViewBag.ArtistImageUrl = user.ImageURL;
            }
            else
            {
                // No user in DB at all
                ViewBag.albumCount = 0;
                ViewBag.WaitingAlbumCount = 0;
                ViewBag.ExpensiveAlbumName = "Yok";
                ViewBag.SongCount = 0;
                ViewBag.Email = "misafir@kullanici.com";
                ViewBag.PhoneNumber = "0000000000";
                ViewBag.IsTwoFactor = false;
                ViewBag.ListenCount = 0;
                ViewBag.ArtistUserName = "Misafir Sanatçı";
                ViewBag.ArtistImageUrl = "/one-music-gh-pages/img/bg-img/a1.jpg";
            }

            return View();
        }
    }
}
