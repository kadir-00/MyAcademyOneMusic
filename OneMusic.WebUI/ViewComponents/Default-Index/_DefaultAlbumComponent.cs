using Microsoft.AspNetCore.Mvc;
using OneMusic.BussinesLayer.Abstarct;

namespace OneMusic.WebUI.ViewComponents.Default_Index
{
    public class _DefaultAlbumComponent(IAlbumService _albumService) : ViewComponent
    {

        public IViewComponentResult Invoke()
        {
           var values = _albumService.TGetAlbumsWithArtists();
            return View(values);

        }
    }
}
