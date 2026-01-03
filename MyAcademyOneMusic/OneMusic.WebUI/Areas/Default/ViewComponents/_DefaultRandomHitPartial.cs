using Microsoft.AspNetCore.Mvc;
using OneMusic.BusinessLayer.Abstract;

namespace OneMusic.WebUI.Areas.Default.ViewComponents
{
    public class _DefaultRandomHitPartial: ViewComponent
    {
        private readonly ISongService _songService;

        public _DefaultRandomHitPartial(ISongService songService)
        {
            _songService = songService;
        }

        public IViewComponentResult Invoke()
        {
            var value = _songService.TgetRandomHitAlbumWithRelationShip();
            return View(value);
        }
    }
}
