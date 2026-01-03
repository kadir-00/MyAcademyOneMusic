using Microsoft.AspNetCore.Mvc;
using OneMusic.BusinessLayer.Abstract;

namespace OneMusic.WebUI.Areas.Default.ViewComponents
{
    public class _DefaultTopOneSingerComponentPartial : ViewComponent
    {
        private readonly ISongService _songService;

        public _DefaultTopOneSingerComponentPartial(ISongService songService)
        {
            _songService = songService;
        }

        public IViewComponentResult Invoke()
        {
            var value = _songService.TGetRandomBestSong();
            return View(value);
        }
    }
}
