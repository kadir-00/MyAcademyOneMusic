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
		private readonly UserManager<AppUser> _userManager;

		public LogInController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager)
		{
			_signInManager = signInManager;
			_userManager = userManager;
		}

		[HttpGet]
		public IActionResult Index()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Index(LoginViewModel model,string? returnUrl)
		{
			var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password,false,false);




            if (result.Succeeded)
            {

				var user = await _userManager.FindByNameAsync(User.Identity.Name);

				var artistResult = await _userManager.IsInRoleAsync(user, "Artist");
				var adminResult = await _userManager.IsInRoleAsync(user, "Admin");

				if (artistResult == true)
				{
					if (returnUrl != null)
					{
						return Redirect(returnUrl);
					}


					return RedirectToAction("Index", "MyAlbum", new { area = "Artist" });
				}

				else if (adminResult == true)
				{
					if (returnUrl != null)
					{
						return Redirect(returnUrl);
					}
					return RedirectToAction("Index", "AdminAbout");
				}

				else
				{
					if (returnUrl != null)
					{
						return Redirect(returnUrl);
					}
					return RedirectToAction("Index", "Default");
				}

			}
			// degilse 
			ModelState.AddModelError("","Kullanici adi veya sifre yanlis");
            return View();
		}


		public async Task<IActionResult> Logout()
		{ 
		 await _signInManager.SignOutAsync();
			return RedirectToAction("Index","Default");
		
		}
	}
}
