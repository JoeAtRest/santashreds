﻿using Microsoft.AspNetCore.Mvc;

namespace ArmadilloParty.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }

        public IActionResult Credits()
        {
            return View();
        }
    }
}
