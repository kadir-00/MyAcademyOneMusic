using Microsoft.AspNetCore.Mvc;
using OneMusic.BusinessLayer.Abstract;
using OneMusic.EntityLayer.Entities;

namespace OneMusic.WebUI.Controllers
{
    public class AlbumsController : Controller
    {
        private readonly IAlbumService _albumService;

        public AlbumsController(IAlbumService albumService)
        {
            _albumService = albumService;
        }

        public IActionResult Index()
        {
            var values = _albumService.TGetList();
            return View(values);
        }

        public IActionResult Details(int id)
        {
            var values = _albumService.TGetAlbumByIdWithSongs(id);
            return View(values);
        }
    }
}
