using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OneMusic.BusinessLayer.Abstract;
using OneMusic.WebUI.ImageSettings;
using X.PagedList;
using OneMusic.EntityLayer.Entities;
using static System.Net.Mime.MediaTypeNames;
using Microsoft.AspNetCore.Identity;

namespace OneMusic.WebUI.Controllers
{
    //[Authorize(Roles = "Admin")]
    public class AdminAlbumVerifyController : Controller
    {
        private readonly IAlbumService _albumService;
        private readonly UserManager<AppUser> _userManager;
        public AdminAlbumVerifyController(IAlbumService albumService, UserManager<AppUser> userManager)
        {
            _albumService = albumService;
            _userManager = userManager;
        }
        public IActionResult Index(int pageNumber = 1)
        {
            var values = _albumService.TgetAlbumWithArtistByStatusFalseList().ToPagedList(pageNumber, 5);
            return View(values);
        }
        public IActionResult RejectAlbums(int pageNumber = 1)
        {
            var values = _albumService.TgetAlbumWithArtistRejectLists().ToPagedList(pageNumber, 5);
            return View(values);
        }
        [HttpPost]
        public async Task<JsonResult> RejectAlbum(int id, string text)
        {
            var value = _albumService.TGetById(id);
            var user = await _userManager.FindByIdAsync(value.AppUserId.ToString());
            value.IsVerify = false;
            value.VerifyDescription = text;
            _albumService.TUpdate(value);
            TempData["Result"] = "İşlem Tamamlandı";
            TempData["icon"] = "success";
            return Json(null);
        }


        public async Task<IActionResult> AcceptAlbum(int id)
        {
            var value = _albumService.TGetById(id);
            var user = await _userManager.FindByIdAsync(value.AppUserId.ToString());
            value.IsVerify = true;
            value.VerifyDescription = "Albümünüz Onaylanmıştır.";
            _albumService.TUpdate(value);
            TempData["Result"] = "İşlem Tamam, Albüm Onaylandı";
            TempData["icon"] = "success";
            return RedirectToAction("Index");
        }

        public IActionResult DeleteAlbum(int id)
        {
            var value = _albumService.TGetById(id);
            _albumService.TDelete(id);
            TempData["Result"] = "Silme işlemi başarılı";
            TempData["icon"] = "success";
            ImageSetting.DeleteImage(value.CoverImage);
            return RedirectToAction("RejectAlbums");
        }
    }
}
