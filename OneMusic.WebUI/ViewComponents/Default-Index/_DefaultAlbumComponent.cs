using Microsoft.AspNetCore.Mvc;
using OneMusic.BussinesLayer.Abstarct;

namespace OneMusic.WebUI.ViewComponents.Default_Index
{
    public class _DefaultAlbumComponent(IAlbumService albumService) : ViewComponent
    {

        public IViewComponentResult Invoke()
        {
            var values = albumService.TGetAlbumsWithArtist();
            return View(values);

        }
    }
}
