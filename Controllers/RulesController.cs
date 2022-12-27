using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace ArmadilloParty.Controllers
{
    [Authorize(Roles = "shredder")]
    public class RulesController : Controller
    {
        Random random = new Random();

        public IActionResult Rules()
        {                       
            return View("rules");
        }
        
    }
}
