﻿using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Contact()
        {
            return View();
        }
    }
}
