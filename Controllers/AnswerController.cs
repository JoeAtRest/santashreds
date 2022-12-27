using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SantaShreds.Support;
using System;
using System.Linq;

namespace ArmadilloParty.Controllers
{
    [Authorize(Roles = "shredder")]
    public class AnswerController : Controller
    {
        Random random = new Random();

        public IActionResult Answer(string txtClue)
        {

            var scorecard = new ScoreCard();
            scorecard.Load(User.Identity.Name);
            var result = scorecard.CompleteLocation(txtClue);

            if (result == "fail")
                return View("ClueFail");

            if (result == "success")
                return View("ClueCompleted");

            if (result == "completed")
                return View("ClueSucess");

            return View("ClueSuccess");
        }

        public IActionResult ClueSuccess()
        {
            return View();
        }

        public IActionResult ClueCompleted()
        {
            return View();
        }

        public IActionResult ClueFail()
        {
            return View();
        }

        public IActionResult Video2()
        {
            return View("2-stokewoods");
        }

        public IActionResult Video3()
        {
            return View("3-fountaindale");
        }

        public IActionResult Video4()
        {
            return View("4-viaduct");
        }

        public IActionResult Video5()
        {
            return View("5-bretton");
        }

        public IActionResult Video6()
        {
            return View("6-hampshire");
        }
    }
}
