using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using WebApplication.Utils.Constants;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        [Route(Routes.Account.Index)]
        public IActionResult Index()
        {
            return View();
        }
        [Route(Routes.Account.Login)]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost(Routes.Account.Login)]
        [AllowAnonymous]
        public async Task<IActionResult> LoginAsync(LoginViewModel model, [FromQuery] string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var claims = new List<Claim>
            {
                new ("Demo", "Value")
            };
            var claimIdentity = new ClaimsIdentity(claims,"cookie");
            var claimPrincipal = new ClaimsPrincipal(claimIdentity);
            await HttpContext.SignInAsync("Cookie", claimPrincipal);
            return RedirectToUrlOrHomePage(returnUrl);
        }
        private IActionResult RedirectToUrlOrHomePage(string url) => IsValidUrl(url) ? Redirect(url) : ReturnToHomepage() ;
        private bool IsValidUrl(string url) => !string.IsNullOrWhiteSpace(url) && Url.IsLocalUrl(url);

        private IActionResult ReturnToHomepage() =>
            RedirectToAction("Index","Home");

        [Route(Routes.Account.Logout)]
        public IActionResult LogOff()
        {
            HttpContext.SignOutAsync("Cookie");
            return Redirect(Routes.Home.Index);
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
        
    }
}
