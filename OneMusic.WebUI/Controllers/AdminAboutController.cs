using Microsoft.AspNetCore.Mvc;
using OneMusic.BussinesLayer.Abstarct;
using OneMusic.EntityLayer.Entities;

namespace OneMusic.WebUI.Controllers
{
    public class AdminAboutController : Controller
    {
        // field ornekliyecez AboutServicden 

        private readonly IAboutService _aboutService;

        public AdminAboutController(IAboutService aboutService)
        {
            _aboutService = aboutService;
        }

        public IActionResult Index()
        {    // LISTLEMEYI YAPACAZ
             var values = _aboutService.TGetList();
            return View(values);
        }

        public IActionResult DeleteAbout(int id )
        {
            _aboutService.TDelete(id);
            return RedirectToAction("Index");

        }

        [HttpGet]
        public IActionResult CreatAbout() 
        {
        return View();
        }

        [HttpPost]
        public IActionResult CreatAbout(About about)
        {
            _aboutService.TCreate(about);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult UpdateAbout(int id)  
        {
        var values = _aboutService.TGetById(id);
            return View(values);
        }

        [HttpPost]
        public IActionResult UpdateAbout(About about)
        { 
        _aboutService.TUpdate(about);
            return RedirectToAction("Index");
        }
    }
}
