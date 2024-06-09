﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OneMusic.BussinesLayer.Abstarct;
using OneMusic.BussinesLayer.Abstract;
using OneMusic.DataAccesLayer.Context;
using OneMusic.EntityLayer.Entities;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace OneMusic.WebUI.Controllers
{
    [AllowAnonymous]
    public class AlbumsController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly UserManager<AppUser> _userManager;
        private readonly IAlbumService _albumService;
        private readonly OneMusicContext _oneMusicContext;

        public AlbumsController(ICategoryService categoryService, UserManager<AppUser> userManager, IAlbumService albumService)
        {
            _categoryService = categoryService;
            _userManager = userManager;
            _albumService = albumService;
        }

        public async Task< IActionResult> Index(object? filterList)
        {
            var categorylist = _categoryService.TGetList();
            var artistlist = await _userManager.GetUsersInRoleAsync("Artist");

            List<SelectListItem> artists = (from x in artistlist
                                            select new SelectListItem
                                            {
                                                Text = x.Name + " " + x.SurName,
                                                Value = x.Name + " " + x.SurName,
                                            }).ToList();
            ViewBag.artists = artists;

            List<SelectListItem> categories = (from x in categorylist
                                               select new SelectListItem
                                               {
                                                   Text = x.CategoryName,
                                                   Value = x.CategoryName,
                                               }).ToList();
            ViewBag.categories = categories;



            if (TempData["filterAlbums"] != null)
            {


                var filterlist = TempData["filterAlbums"].ToString();

                var albums = JsonSerializer.Deserialize<List<Album>>(filterlist, new JsonSerializerOptions
                {
                    ReferenceHandler = ReferenceHandler.IgnoreCycles
                });
                if (albums != null)
                {
                    return View(albums);
                }

            }

            var values = _albumService.TGetAlbumsWithArtist();
            return View(values);
        }

        [HttpGet]
        public async Task< PartialViewResult> FilterAlbums()
        {
            var categorylist = _categoryService.TGetList();
            var artistlist = await _userManager.GetUsersInRoleAsync("Artist");

            List<SelectListItem> artists = (from x in artistlist
                                            select new SelectListItem
                                            { 
                                            Text = x.Name + " " + x.SurName,
                                            Value = x.Id.ToString()
                                            }).ToList();
            ViewBag.artists = artists;

            List<SelectListItem> categories = (from x in categorylist
                                            select new SelectListItem
                                            {
                                                Text = x.CategoryName,
                                                Value = x.CategoryName
                                            }).ToList();
            ViewBag.categories = categories;

            return  PartialView();
        }

        [HttpPost]
        public IActionResult FilterAlbums(string category,string artist)
        {

            if (string.IsNullOrEmpty(category) || string.IsNullOrEmpty(artist) )
            {
                var values = _oneMusicContext.Albums.Include(x=>x.AppUser).Include(x=>x.Category).Where(x => x.Category.CategoryName == category && x.AppUser.Name +
                "" + x.AppUser.SurName == artist).ToList();

                TempData["filterAlbums"] = JsonSerializer.Serialize(values, new JsonSerializerOptions
                {
                    ReferenceHandler = ReferenceHandler.IgnoreCycles
                });
            }

            return RedirectToAction("Index");
        }
    }
}
