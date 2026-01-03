using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OneMusic.BusinessLayer.Abstract;
using OneMusic.EntityLayer.Entities;
using OneMusic.WebUI.ImageSettings;
using OneMusic.WebUI.Models.SongModels;

namespace OneMusic.WebUI.Areas.Artist.Controllers
{
    [Area("Artist")]
    //[Authorize(Roles = "Artist")]
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

        public async Task<IActionResult> Index()
        {
            AppUser user = null;
            if (User.Identity?.Name != null)
            {
                user = await _userManager.FindByNameAsync(User.Identity.Name);
            }

            if (user == null)
            {
                user = _userManager.Users.FirstOrDefault();
            }

            if (user != null)
            {
                var values = _songService.TArtistSongsWithAlbum(user.Id);
                return View(values);
            }

            // Fallback for no user
            return View(new List<Song>());
        }

        async Task<List<SelectListItem>> loadDropdown()
        {
            AppUser user = null;
            if (User.Identity?.Name != null)
            {
                user = await _userManager.FindByNameAsync(User.Identity.Name);
            }

            if (user == null)
            {
                user = _userManager.Users.FirstOrDefault();
            }

            int userId = user != null ? user.Id : 0;

            var values = _albumService.TgetAlbumByArtist(userId);
            List<SelectListItem> selectListItems = (from x in values
                                                    select new SelectListItem
                                                    {
                                                        Text = x.AlbumName,
                                                        Value = x.AlbumId.ToString(),
                                                    }).ToList();
            ViewBag.AlbumList = selectListItems;
            return selectListItems;
        }



        [HttpGet]
        public async Task<IActionResult> CreateSong()
        {
            await loadDropdown();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateSong(CreateSongViewModel song)
        {
            if (ModelState.IsValid)
            {
                string ex = Path.GetExtension(song.SongFile.FileName);
                if (ex == ".mp3")
                {
                    var songFile = ImageSetting.CreateSong(song.SongFile);
                    _songService.TCreate(new Song
                    {
                        AlbumId = song.AlbumId,
                        SongName = song.SongName,
                        SongUrl = songFile,

                    });
                    TempData["Result"] = "Kayıt eklendi";
                    TempData["icon"] = "success";
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("SongFile", "mp3 dosyası seçiniz");
                    await loadDropdown();
                    return View();
                }

            }
            else
            {
                await loadDropdown();
                return View();
            }
        }


        [HttpGet]
        public async Task<IActionResult> UpdateSong(int id)
        {
            var value = _songService.TGetById(id);
            UpdateSongViewModel updateSongViewModel = new UpdateSongViewModel()
            {
                AlbumId = value.AlbumId,
                SongName = value.SongName,
                SongUrl = value.SongUrl,
                SongId = value.SongId,

            };
            await loadDropdown();
            return View(updateSongViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateSong(UpdateSongViewModel song)
        {
            var value = _songService.TGetById(song.SongId);
            value.SongName = song.SongName;
            value.AlbumId = song.AlbumId;
            if (song.SongFile != null)
            {
                var songFileName = ImageSetting.CreateSong(song.SongFile);
                value.SongUrl = songFileName;
            }
            _songService.TUpdate(value);
            TempData["Result"] = "Şarkı Güncellendi";
            TempData["icon"] = "success";
            return RedirectToAction("Index");

        }

        public IActionResult DeleteSong(int id)
        {
            _songService.TDelete(id);
            TempData["Result"] = "Şarkı Silindi";
            TempData["icon"] = "success";
            return RedirectToAction("Index");
        }
    }
}
