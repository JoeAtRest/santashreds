﻿using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ArmadilloParty.ViewModels;
using System.Linq;
using System.Security.Claims;
using Auth0.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using SantaShreds.Support;
using SantaShreds.Models;

namespace ArmadilloParty.Controllers
{
    public class AccountController : Controller
    {
        public async Task Login(string returnUrl = "/")
        {
            
            var authenticationProperties = new LoginAuthenticationPropertiesBuilder()                
                .WithRedirectUri(returnUrl)
                .Build();

            await HttpContext.ChallengeAsync(Auth0Constants.AuthenticationScheme, authenticationProperties);
        }

        [Authorize]
        public async Task Logout()
        {
            var authenticationProperties = new LogoutAuthenticationPropertiesBuilder()
                // Indicate here where Auth0 should redirect the user after a logout.
                // Note that the resulting absolute Uri must be whitelisted in 
                .WithRedirectUri(Url.Action("Index", "Home"))
                .Build();

            await HttpContext.SignOutAsync(Auth0Constants.AuthenticationScheme, authenticationProperties);
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }

        [Authorize]
        public IActionResult Profile()
        {
            return View(new UserProfileViewModel()
            {
                Name = User.Identity.Name,
                EmailAddress = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value,
                ProfileImage = User.Claims.FirstOrDefault(c => c.Type == "picture")?.Value
            });
        }


        /// <summary>
        /// This is just a helper action to enable you to easily see all claims related to a user. It helps when debugging your
        /// application to see the in claims populated from the Auth0 ID Token
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "shredadmin")]
        public IActionResult Claims()
        {
            return View();
        }
        
        public IActionResult AccessDenied()
        {
            return View();
        }

        [Authorize(Roles = "shredadmin")]
        public IActionResult Checkup()
        {
            var scorecard = new ScoreCard();
            scorecard.Load("player1@santas-fiendish-secret.co.uk");            
            return View(scorecard.card);
        }

        [Authorize(Roles = "shredadmin")]
        public IActionResult Edit(string id)
        {
            var scorecard = new ScoreCard();
            scorecard.Load("player1@santas-fiendish-secret.co.uk");
            var loc = scorecard.card.GetLocation(id);
            return View("View",loc);
        }

        [Authorize(Roles = "shredadmin")]
        public IActionResult View(Location loc)
        {
            var scorecard = new ScoreCard();
            scorecard.Load("player1@santas-fiendish-secret.co.uk");
            scorecard.card.UpdateLocation(loc);
            scorecard.Update();
            return View("Checkup",scorecard.card);
        }
    }
}
