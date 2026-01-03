using Microsoft.AspNetCore.Mvc;
using OneMusic.BusinessLayer.Abstract;

namespace OneMusic.WebUI.Areas.Default.ViewComponents
{
    public class _DefaultSingerComponentPartial : ViewComponent
    {
        private readonly ISongService _songService;

        public _DefaultSingerComponentPartial(ISongService songService)
        {
            _songService = songService;
        }

        public IViewComponentResult Invoke()
        {
            var value = _songService.TgetRandomSingerWithRelationShip();
            return View(value);
        }
    }
}
