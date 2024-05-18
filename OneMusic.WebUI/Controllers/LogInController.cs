using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OneMusic.EntityLayer.Entities;
using OneMusic.WebUI.Models;

namespace OneMusic.WebUI.Controllers
{ // sadece Yetkili Kisiler Admin Tarafina Erisebilecek sayfalara bunun icin asagudakini de yazmamiz gerekiypr ,
  // asagidaki sayesinde burasi authorizedan muaf olacak 
	[AllowAnonymous]
	public class LogInController : Controller
	{
		private readonly SignInManager<AppUser> _signInManager;

		public LogInController(SignInManager<AppUser> signInManager)
		{
			_signInManager = signInManager;
		}

		[HttpGet]
		public IActionResult Index()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Index(LoginViewModel model)
		{
			var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password,false,false);
            if (result.Succeeded)
            {
                return RedirectToAction("Index","AdminAbout");
            }
			// degilse 
			ModelState.AddModelError("","Kullanici adi veya sifre yanlis");
            return View();
		}
	}
}
