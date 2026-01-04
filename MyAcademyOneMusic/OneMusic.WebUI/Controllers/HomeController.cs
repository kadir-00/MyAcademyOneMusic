using Microsoft.AspNetCore.Mvc;
using OneMusic.WebUI.Models;
using System.Diagnostics;
using OneMusic.BusinessLayer.Abstract;
using OneMusic.EntityLayer.Entities;
using Microsoft.AspNetCore.Identity;

namespace OneMusic.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IAlbumService _albumService;
        private readonly ISongService _songService;
        private readonly UserManager<AppUser> _userManager;

        public HomeController(ILogger<HomeController> logger, IAlbumService albumService, ISongService songService, UserManager<AppUser> userManager)
        {
            _logger = logger;
            _albumService = albumService;
            _songService = songService;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var latestAlbums = _albumService.TGetList();
            var popularArtists = await _userManager.GetUsersInRoleAsync("Artist");
            var model = new HomeViewModel
            {
                LatestAlbums = latestAlbums,
                BuyNowAlbums = latestAlbums, // Temporary logic as discussed
                FeaturedAlbum = latestAlbums.LastOrDefault(), // Temporary logic
                PopularArtists = popularArtists.Take(4).ToList(),
                NewHitSongs = _songService.TGetSongsOrderedByDate(2, 6),
                WeeksTopAlbums = _albumService.TGetAlbumsOrderedByDate(3, 6)
            };

            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
