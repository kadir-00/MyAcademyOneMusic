using Microsoft.AspNetCore.Mvc;
using OneMusic.BusinessLayer.Abstract;

namespace OneMusic.WebUI.Areas.Default.ViewComponents
{
    public class _DefaultAlbumComponentPartial : ViewComponent
    {
        private readonly IAlbumService _albumService;

        public _DefaultAlbumComponentPartial(IAlbumService albumService)
        {
            _albumService = albumService;
        }

        public IViewComponentResult Invoke()
        {
            var values = _albumService.TgetAlbumListWithArtist();
            return View(values);
        }
    }
}
