using Microsoft.AspNetCore.Mvc;
using OneMusic.BusinessLayer.Abstract;

namespace OneMusic.WebUI.Areas.Default.ViewComponents
{
    public class _DefaultHitAlbumComponentPartial : ViewComponent
    {
        private readonly IAlbumService _albumService;

        public _DefaultHitAlbumComponentPartial(IAlbumService albumService)
        {
            _albumService = albumService;
        }

        public IViewComponentResult Invoke()
        {
            var value = _albumService.TgetRandomAlbumWithArtist();
            return View(value);
        }
    }
}
