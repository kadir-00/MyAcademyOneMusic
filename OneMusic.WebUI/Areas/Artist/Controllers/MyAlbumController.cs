﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OneMusic.BussinesLayer.Abstarct;
using OneMusic.EntityLayer.Entities;

namespace OneMusic.WebUI.Areas.Artist.Controllers
{
    [Area("Artist")] // Buranin area icerisinde oldugunu belirtmemiz lazim belirttik

    [Authorize(Roles = "Artist")] // Sadece Rolu artist olan kisiler bu sayfaya erisebilirler

    [Route("[area]/[controller]/[action]/{id?}")]

    // Asagida yaptigimiz Primary Constucter ctrl nokta yapmadan bu sekilde de yapilabiliyor , ve program.cs de registration yapman lazim ayni sekil 
    public class MyAlbumController(IAlbumService _albumService, UserManager<AppUser> _userManager) : Controller
    {

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var userid = user.Id;

            var values = _albumService.TGetAlbumsByArtist(userid);
            return View(values);
        }

        [HttpGet]
        public IActionResult CreateAlbum()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateAlbum(Album album)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            album.AppUserId = user.Id;

            _albumService.TCreate(album);
            return RedirectToAction("Index");
        }

        public IActionResult DeleteAlbum(int id)
        {
            _albumService.TDelete(id);
            return RedirectToAction("Index");

        }

        [HttpGet]
        public IActionResult UpdateAlbum(int id)
        {
            var values = _albumService.TGetById(id);
            return View(values);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateAlbum(Album album)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            album.AppUserId = user.Id;

            _albumService.TUpdate(album);
            return RedirectToAction("Index");
        }
    }
 }
