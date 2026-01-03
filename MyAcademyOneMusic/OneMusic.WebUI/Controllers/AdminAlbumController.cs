using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json.Linq;
using OneMusic.BusinessLayer.Abstract;
using OneMusic.BusinessLayer.Models.Album;
using OneMusic.BusinessLayer.ValidationRules;
using OneMusic.EntityLayer.Entities;
using OneMusic.WebUI.DAL;
using OneMusic.WebUI.ImageSettings;
using X.PagedList;

namespace OneMusic.WebUI.Controllers
{
    //[Authorize(Roles = "Admin")]

    public class AdminAlbumController : Controller
    {
        private readonly IAlbumService _albumService;
        public AdminAlbumController(IAlbumService albumService)
        {
            _albumService = albumService;
        }

        public IActionResult Index(int pageNumber = 1)
        {
            var values = _albumService.TgetAlbumListWithArtist().ToPagedList(pageNumber,5);
            return View(values);
        }
        public IActionResult DeleteAlbum(int id)
        {
            var value = _albumService.TGetById(id);
            _albumService.TDelete(id);
            TempData["Result"] = "Silme işlemi başarılı";
            TempData["icon"] = "success";
            ImageSetting.DeleteImage(value.CoverImage);
            return RedirectToAction("Index");
        }

    }
}
