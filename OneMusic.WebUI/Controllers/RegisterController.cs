using Azure.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OneMusic.EntityLayer.Entities;
using OneMusic.WebUI.Models;

namespace OneMusic.WebUI.Controllers
{ // sadece Yetkili Kisiler Admin Tarafina Erisebilecek sayfalara bunun icin asagudakini de yazmamiz gerekiypr ,
  // asagidaki sayesinde burasi authorizedan muaf olacak 
	[AllowAnonymous]
    public class RegisterController : Controller
    {
        private readonly UserManager<AppUser> _userManager;

        public RegisterController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Signup()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult>  Signup(RegisterViewModel model)
        {
            AppUser user = new AppUser
            {
                Email = model.Email,
                UserName = model.UserName,
                Name= model.Name,
                SurName= model.SurName,

            
            };

            if (model.Password==model.ConfirmPassword)
            {
				var result = await _userManager.CreateAsync(user, model.Password);
				if (result.Succeeded) // eger sonuc basarili olursa  
				{
                    await _userManager.AddToRoleAsync(user, "Visiter");
					return RedirectToAction("Index", "Login");
				}
                foreach (var item in result.Errors) // yandaki foreach'i resolts.errors.foreach yaparak getirdik
                {
					ModelState.AddModelError("", item.Description);
				}
            }
			
			return View();
        }
    }
}
