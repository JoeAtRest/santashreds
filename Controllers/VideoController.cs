using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SantaShreds.Models;
using SantaShreds.Support;
using System;

namespace ArmadilloParty.Controllers
{
    [Authorize(Roles = "shredder")]
    public class VideoController : Controller
    {
        Random random = new Random();

        public IActionResult Video1()
        {
            var scorecard = new ScoreCard();
            scorecard.Load(User.Identity.Name);
            var c = new Completion()
            {
                IsCompleted = scorecard.QueryLocation("BIK")
            };

            return View("1-kingsheath",c);
        }

        public IActionResult Video2()
        {
            var scorecard = new ScoreCard();
            scorecard.Load(User.Identity.Name);
            var c = new Completion()
            {
                IsCompleted = scorecard.QueryLocation("COH")
            };
            return View("2-stokewoods",c);
        }

        public IActionResult Video3()
        {
            var scorecard = new ScoreCard();
            scorecard.Load(User.Identity.Name);
            var c = new Completion()
            {
                IsCompleted = scorecard.QueryLocation("TWO")
            };
            return View("3-fountaindale",c);
        }

        public IActionResult Video4()
        {
            var scorecard = new ScoreCard();
            scorecard.Load(User.Identity.Name);
            var c = new Completion()
            {
                IsCompleted = scorecard.QueryLocation("BAK")
            };
            return View("4-viaduct",c);
        }

        public IActionResult Video5()
        {
            var scorecard = new ScoreCard();
            scorecard.Load(User.Identity.Name);
            var c = new Completion()
            {
                IsCompleted = scorecard.QueryLocation("FIN")
            };

            if (scorecard.card.FinalLocationUnlocked.HasValue && scorecard.card.FinalLocationUnlocked.Value)
            {
                return View("5-bretton", c);
            }

            return View("1-kingsheath", c);
        }

        public IActionResult Video6()
        {
            var scorecard = new ScoreCard();
            scorecard.Load(User.Identity.Name);
            var c = new Completion()
            {
                IsCompleted = scorecard.QueryLocation("LNJ")
            };
            return View("6-hampshire",c);
        }

        public IActionResult Video7()
        {
            var scorecard = new ScoreCard();
            scorecard.Load(User.Identity.Name);
            var c = new Completion()
            {
                IsCompleted = scorecard.QueryLocation("LNJ")
            };
            return View("7-rules", c);
        }
    }
}
