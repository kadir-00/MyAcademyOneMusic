using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OneMusic.EntityLayer.Entities;

namespace OneMusic.WebUI.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RoleController : Controller
    {
        private readonly RoleManager<AppRole> _roleManager;

        public RoleController(RoleManager<AppRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public IActionResult Index()
        {
            var values = _roleManager.Roles.ToList();
            return View(values);
        }
        // ekleme 
        [HttpGet]
        public IActionResult CreateRole() 
        {

        return View();
        
        }

        [HttpPost]
        // asagi method'ta sadece awaot kullanamayiz metodun async olmasi lazim.Kisa yoldan yapimi , await'e ctrl+nokta yap ordan secerek yapabilirsin ,
        //  CreateRole'un sonundaki async'ki sildik cunku digeri ile ayni olmasi lazim
        public async Task<IActionResult> CreateRole(AppRole role)
        {
           await _roleManager.CreateAsync(role);
           return RedirectToAction("Index");

        }
        // ekleme bitti

        // silme 
        public async Task<IActionResult> DeleteRole(int id)
        {
            var value = _roleManager.Roles.FirstOrDefault(x => x.Id == id);
            await _roleManager.DeleteAsync(value);
            return RedirectToAction("Index");

        }
        // silme islemi bitti

        //Guncelleme 
        public IActionResult UpdateRole(int id) 
        {
            var value = _roleManager.Roles.FirstOrDefault(x=>x.Id == id);
            return View(value);
        }

        [HttpPost]
         public async Task<IActionResult> UpdateRole(AppRole role) 
        {
        await _roleManager.UpdateAsync(role);
            return RedirectToAction("Index");
        
        }
        //Guncelleme islemi bitti
    }
}
