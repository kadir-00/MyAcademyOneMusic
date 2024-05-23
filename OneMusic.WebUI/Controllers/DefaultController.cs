﻿using Microsoft.AspNetCore.Mvc;
using OneMusic.BussinesLayer.Abstarct;
using OneMusic.EntityLayer.Entities;

namespace OneMusic.WebUI.Controllers
{
    public class DefaultController(IMessageService _messageService) : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SendMessage(Message message)
        {
            _messageService.TCreate(message);
            return NoContent();
        
        }
    }
}
