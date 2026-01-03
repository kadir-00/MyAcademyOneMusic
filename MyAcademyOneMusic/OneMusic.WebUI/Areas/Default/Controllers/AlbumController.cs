using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OneMusic.BusinessLayer.Abstract;
using OneMusic.DataAccessLayer.Context;
using OneMusic.EntityLayer.Entities;
using System.Text.Json;
using System.Text.Json.Serialization;
using X.PagedList;

namespace OneMusic.WebUI.Areas.Default.Controllers
{
    [Area("Default")]
    [Route("[area]/[controller]/[action]/{id?}")]
    [AllowAnonymous]
    public class AlbumController : Controller
    {
        private readonly IAlbumService _albumService;
        private readonly ICategoryService _categoryService;
        private readonly UserManager<AppUser> _usermanager;
        public AlbumController(IAlbumService albumService, ICategoryService categoryService, UserManager<AppUser> usermanager)
        {
            _albumService = albumService;
            _categoryService = categoryService;
            _usermanager = usermanager;
        }

        async Task loadDropdopwn()
        {
            var categoryList = _categoryService.TGetList();
            var artistList = await _usermanager.GetUsersInRoleAsync("Artist");


            List<SelectListItem> Artists = (from x in artistList
                                            select new SelectListItem
                                            {
                                                Text = x.Name + " " + x.Surname,
                                                Value = x.Name + " " + x.Surname,
                                            }).ToList();
            ViewBag.Artitst = Artists;


            List<SelectListItem> Categories = (from x in categoryList
                                               select new SelectListItem
                                               {
                                                   Text = x.CategoryName,
                                                   Value = x.CategoryName,
                                               }).ToList();
            ViewBag.Categorys = Categories;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int pageNumber = 1)
        {
            var values = _albumService.TgetAlbumListWithArtist().ToPagedList(pageNumber, 12);
            await loadDropdopwn();

            return View(values);
        }
        [HttpPost]
        public async Task<IActionResult> Index(string category, string artist)
        {

            //burası mimariye taşınacak
            if (category != "Tür Seçin" && artist != "Sanatçı Seçin")
            {
                var values = _albumService.TgetListAlbumWithCategoryAndArtist(category, artist).ToPagedList(1, 12);
                await loadDropdopwn();
                return View("Index", values);
            }
            else
            {
                if (category != "Tür Seçin")
                {
                    var values = _albumService.TgetListAlbumWithCategory(category).ToPagedList(1, 12);
                    await loadDropdopwn();
                    return View("Index", values);
                }

                if (artist != "Sanatçı Seçin")
                {
                    var values = _albumService.TgetListAlbumWithArtist(artist).ToPagedList(1, 12);
                    await loadDropdopwn();
                    return View("Index", values);
                }

                return View();
            }


        }
    }
}
