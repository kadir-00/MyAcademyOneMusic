using Microsoft.AspNetCore.Mvc;
using OneMusic.DataAccessLayer.Abstract;
using OneMusic.EntityLayer.Entities;
using OneMusic.WebUI.ImageSettings;
using OneMusic.WebUI.Models.EventModels;

using Microsoft.AspNetCore.Authorization;

namespace OneMusic.WebUI.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminEventsController : Controller
    {
        private readonly IEventDal _eventDal;

        public AdminEventsController(IEventDal eventDal)
        {
            _eventDal = eventDal;
        }

        public IActionResult Index()
        {

            var value = _eventDal.GetList();
            return View(value);
        }
        [HttpGet]
        public IActionResult CreateEvent()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateEvent(CreateEventViewModel model)
        {
            var result = ImageSetting.CreateImage(model.FormFile, "Events");
            _eventDal.Create(new Event
            {
                Date = model.Date,
                Location = model.Location,
                Title = model.Title,
                ImageURL = result,
            });
            TempData["Result"] = "Kayıt Edildi";
            TempData["icon"] = "success";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult UpdateEvent(int id)
        {
            var value = _eventDal.GetById(id);
            UpdateEventViewModel updateEventViewModel = new UpdateEventViewModel()
            {
                Date = value.Date,
                Location = value.Location,
                Title = value.Title,
                ImageURL = value.ImageURL,
                EventID = value.EventID,
            };
            return View(updateEventViewModel);
        }
        [HttpPost]
        public IActionResult UpdateEvent(UpdateEventViewModel model)
        {

            var value = _eventDal.GetById(model.EventID);
            if (model.FormFile != null)
            {
                var result = ImageSetting.CreateImage(model.FormFile, "Events");
                value.ImageURL = result;
            }
            value.Date = model.Date;
            value.Location = model.Location;
            value.Title = model.Title;
            _eventDal.Update(value);
            TempData["Result"] = "Kayıt Güncellendi";
            TempData["icon"] = "success";
            return RedirectToAction("Index");
        }
        public IActionResult DeleteEvent(int id)
        {
            _eventDal.Delete(id);
            TempData["Result"] = "Kayıt Silindi";
            TempData["icon"] = "success";
            return RedirectToAction("Index");
        }

    }
}
