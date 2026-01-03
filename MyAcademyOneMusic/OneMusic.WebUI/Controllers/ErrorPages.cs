using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace OneMusic.WebUI.Controllers
{
    [AllowAnonymous]
    public class ErrorPages : Controller
    {
        public IActionResult Page403()
        {
            return View();
        }
		public IActionResult Page404()
		{
			return View();
		}
	}
}
