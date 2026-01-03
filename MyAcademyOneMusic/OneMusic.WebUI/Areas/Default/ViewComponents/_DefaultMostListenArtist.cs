using Microsoft.AspNetCore.Mvc;
using OneMusic.BusinessLayer.Abstract;

namespace OneMusic.WebUI.Areas.Default.ViewComponents
{
    public class _DefaultMostListenArtist : ViewComponent
    {
        private readonly ISongsListenDetailsService _SongListenService;

        public _DefaultMostListenArtist(ISongsListenDetailsService songListenService)
        {
            _SongListenService = songListenService;
        }

        public IViewComponentResult Invoke()
        {
            var value = _SongListenService.TgetMostListenSingers();
            return View(value);
        }
    }
}
