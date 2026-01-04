using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OneMusic.BusinessLayer.Abstract;
using OneMusic.BusinessLayer.Models.Album;
using OneMusic.BusinessLayer.ValidationRules;
using OneMusic.EntityLayer.Entities;
using OneMusic.WebUI.ImageSettings;

namespace OneMusic.WebUI.Areas.Artist.Controllers
{
    [Area("Artist")]
    [Authorize(Roles = "Artist")]
    [Route("[area]/[controller]/[action]/{id?}")]
    public class MyAlbumController : Controller
    {
        private readonly IAlbumService _albumService;
        private readonly UserManager<AppUser> _userManager;
        private readonly ICategoryService _categoryService;

        public MyAlbumController(IAlbumService albumService, UserManager<AppUser> userManager, ICategoryService categoryService)
        {
            _albumService = albumService;
            _userManager = userManager;
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index()
        {
            var userName = User.Identity?.Name;
            AppUser? user = null;

            if (!string.IsNullOrEmpty(userName))
            {
                user = await _userManager.FindByNameAsync(userName);
            }

            if (user == null)
            {
                user = _userManager.Users.FirstOrDefault();
            }

            if (user != null)
            {
                var values = _albumService.TgetAlbumByArtist(user.Id);
                return View(values);
            }

            // Fallback for no user at all (empty list)
            return View(new List<Album>());
        }

        async Task<List<SelectListItem>> loadDropdown()
        {
            var userName = User.Identity?.Name;
            AppUser? user = null;

            if (!string.IsNullOrEmpty(userName))
            {
                user = await _userManager.FindByNameAsync(userName);
            }

            if (user == null)
            {
                user = _userManager.Users.FirstOrDefault();
            }

            // Fallback for null user - pass dummy Id 0 or handle logic
            int userId = user != null ? user.Id : 0;

            var values = _albumService.TgetAlbumByArtist(userId);
            List<SelectListItem> selectListItems = (from x in values
                                                    select new SelectListItem
                                                    {
                                                        Text = x.AlbumName,
                                                        Value = x.AlbumId.ToString(),
                                                    }).ToList();
            ViewBag.AlbumList = selectListItems;


            var categoryList = _categoryService.TGetList();

            List<SelectListItem> categories = (from x in categoryList
                                               select new SelectListItem
                                               {
                                                   Text = x.CategoryName,
                                                   Value = x.CategoryID.ToString(),
                                               }).ToList();
            ViewBag.categories = categories;

            return selectListItems;
        }

        [HttpGet]
        public async Task<IActionResult> CreateAlbum()
        {
            await loadDropdown();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateAlbum(CreateAlbumViewModel album)
        {
            ModelState.Clear();

            var userName = User.Identity?.Name;
            AppUser? user = null;
            if (!string.IsNullOrEmpty(userName))
            {
                user = await _userManager.FindByNameAsync(userName);
            }

            if (user == null)
            {
                user = _userManager.Users.FirstOrDefault();
            }

            if (user == null)
            {
                // No user found to assign the album to
                TempData["Result"] = "Kullanıcı bulunamadı, işlem yapılamaz.";
                TempData["icon"] = "error";
                await loadDropdown();
                return View();
            }

            if (album.Image != null)
            {


                var result = ImageSetting.CreateImage(album.Image, "Albums");
                _albumService.TCreate(new Album
                {
                    AlbumName = album.AlbumName,
                    AppUserId = user.Id,
                    IsVerify = false,
                    VerifyDescription = "Onay Aşamasında",
                    Price = album.Price,
                    CoverImage = result,
                    CategoryID = album.CategoryID


                });

                TempData["Result"] = "Albümünüz kaydedildi, onay işlemini 'Başvurularım' sekmesinden takip edebilirsiniz";
                TempData["icon"] = "success";
                return RedirectToAction("Index");


            }
            else
            {
                TempData["Result"] = "Görsel Seçiniz";
                TempData["icon"] = "info";

            }
            await loadDropdown();
            return View();


        }

        [HttpGet]
        public IActionResult UpdateAlbum(int id)
        {
            var values = _albumService.TGetById(id);

            UpdateAlbumViewModel updateAlbumViewModel = new UpdateAlbumViewModel()
            {
                AlbumId = values.AlbumId,
                AlbumName = values.AlbumName,
                AppUserId = values.AppUserId,
                CoverImage = values.CoverImage,
                Price = values.Price,

            };
            return View(updateAlbumViewModel);
        }
        [HttpPost]
        public ActionResult UpdateAlbum(UpdateAlbumViewModel album)
        {
            var value = _albumService.TGetById(album.AlbumId);

            value.AlbumName = album.AlbumName ?? value.AlbumName; // Keep old name if null, or empty. Better to keep old? Or just empty. Viewmodel binding usually sends null if empty? No, required was warning before.
            // If I made ViewModel nullable, I should check if it's null.
            if (album.AlbumName != null) value.AlbumName = album.AlbumName;

            value.Price = album.Price;

            if (album.Image != null)
            {
                var result = ImageSetting.CreateImage(album.Image, "Albums");
                value.CoverImage = result ?? value.CoverImage;
            }



            _albumService.TUpdate(value);
            TempData["Result"] = "Albümünüz Güncellendi.";
            TempData["icon"] = "success";
            return RedirectToAction("Index");
        }

        public IActionResult DeleteAlbum(int id)
        {
            _albumService.TDelete(id);
            TempData["Result"] = "Albümünüz Silindi.";
            TempData["icon"] = "success";
            return RedirectToAction("Index");
        }
    }
}
