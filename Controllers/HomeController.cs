using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SantaShreds.Support;

namespace ArmadilloParty.Controllers
{
    public class HomeController : Controller
    {
        [Authorize(Roles = "shredder")]
        public IActionResult Index()
        {
            var scorecard = new ScoreCard();
            scorecard.Load("player1@santas-fiendish-secret.co.uk");
            var progress = scorecard.CheckProgress();            

            return View("Index", progress);
        }

        [Authorize(Roles = "shredder")]
        public IActionResult Completion()
        {
            var scorecard = new ScoreCard();
            scorecard.Load("player1@santas-fiendish-secret.co.uk");
            var progress = scorecard.QueryProgress();
            return PartialView("Completion",new { Clues = progress.Item1, Tricks = progress.Item2 });
        }

        [Authorize(Roles = "shredder")]
        public IActionResult Error()
        {
            return View();
        }

        [Authorize(Roles = "shredder")]
        public IActionResult Credits()
        {
            return View();
        }
    }
}
