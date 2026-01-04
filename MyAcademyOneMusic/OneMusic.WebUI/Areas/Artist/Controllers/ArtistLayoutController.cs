using Microsoft.AspNetCore.Mvc;

using Microsoft.AspNetCore.Authorization;

namespace OneMusic.WebUI.Areas.Artist.Controllers
{
    [Authorize(Roles = "Artist")]
    public class ArtistLayoutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
