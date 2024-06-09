using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OneMusic.BussinesLayer.Abstarct;
using OneMusic.EntityLayer.Entities;
using OneMusic.WebUI.Areas.Artist.Models;

namespace OneMusic.WebUI.Areas.Artist.Controllers
{
    [Area("Artist")]

    [Authorize(Roles = "Artist")] // Sadece Rolu artist olan kisiler bu sayfaya erisebilirler

    [Route("[area]/[controller]/[action]/{id?}")] 
    public class MySongController : Controller
    {
        private readonly ISongService _songService;

        private readonly UserManager<AppUser> _userManager;
        private readonly IAlbumService _albumService;

        public MySongController(ISongService songService, UserManager<AppUser> userManager, IAlbumService albumService)
        {
            _songService = songService;
            _userManager = userManager;
            _albumService = albumService;
        }

        public async Task< IActionResult > Index()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var values = _songService.TGetSongsWithAlbumByUserId(user.Id);
            return View(values);
        }

        [HttpGet]
        public async Task< IActionResult>  CreateSong() 
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var albumlist = _albumService.TGetAlbumsByArtist(user.Id);
            List<SelectListItem> albums = (from x in albumlist
                                           select new SelectListItem
                                           {
                                               Text = x.AlbumName,
                                               Value = x.AlbumId.ToString()

                                           }).ToList();
            ViewBag.albums = albums;
           return View();
                 
        }
        [HttpPost]
        public async Task< IActionResult> CreateSong(SongViewModel model)
        {

            var song = new Song
            {
                SongName = model.SongName,
                AlbumID = model.AlbumID,


            };


            if (model.SongFile != null) 
            {
                var resource = Directory.GetCurrentDirectory(); // Suanki projenin yolunu bul diyoruz 
                var extansion = Path.GetExtension(model.SongFile.FileName).ToLower(); // sectigimiz dosyanin uzantisini aldirdik
                if (extansion != ".mp3" )
                {
                    // desteklenmeyen dosya uzantisi hatasi 
                    ModelState.AddModelError("SongFile", "Sadece MP3 DOSYALARI");
                    // GEREKIRSE Islem Sonlandirma 
                    return View(model);
                }
                var songName = Guid.NewGuid() + extansion;    // dosyanin ismini aliyorruz 
                var saveLocation = resource + "/wwwroot/songs/" + songName;     //kaydedecegimiz yer
                var stream = new FileStream(saveLocation, FileMode.Create); // kaydetme islemi
                await model.SongFile.CopyToAsync(stream);
                song.SongUrl = "/Songs/" + songName;
            }

            _songService.TCreate(song);
           
            return RedirectToAction("Index");
        }
    }
}
