using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Data.Entities;
using WebApplication.Utils.Constants;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }
        [Authorize]
        [HttpGet(Routes.Account.Index)]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet(Routes.Account.SignUp)]
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost(Routes.Account.SignUp)]
        public async Task<IActionResult> SignUpAsync([FromForm]SignUpViewModel model, [FromQuery(Name = ApplicationCookies.ReturnUrlParameter)]string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            User user = new User
            {
                UserName = model.UserName,
                Email = model.Email,
                RegistrationDate = DateTime.Now,
                LastAuthorization = DateTime.Now
            };
            var result = await userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                await signInManager.SignInAsync(user, isPersistent: true);
                return RedirectToUrlOrDefault(returnUrl);
            }
            return View(model);
        }

        [HttpGet(Routes.Account.SignIn)]
        public IActionResult Signin()
        {
            return View();
        }

        [HttpPost(Routes.Account.SignIn)]
        public async Task<IActionResult> SignInAsync([FromForm]SignInViewModel model, [FromQuery(Name = ApplicationCookies.ReturnUrlParameter)] string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await userManager.FindByNameAsync(model.UserName);
            if (user == null)
            {
                ModelState.AddModelError("","Username is not found");
                return View(model);
            }

            var result = await signInManager.PasswordSignInAsync(user, model.Password, isPersistent:true,lockoutOnFailure:false);
            if (result.Succeeded)
            {
                user.LastAuthorization = DateTime.Now;
                var editResult = await userManager.UpdateAsync(user);
                if (editResult.Succeeded)
                {
                    return RedirectToUrlOrDefault(returnUrl);
                }
            }
            return View(model);
        }

        [Authorize]
        [HttpPost(Routes.Account.SignOut)]
        public async Task<IActionResult> SignOutAsync()
        {
            await signInManager.SignOutAsync();
            return Redirect(Routes.Home.Index);
        }

        [Authorize]
        public IActionResult AccessDenied()
        {
            return View();
        }

        private IActionResult RedirectToUrlOrDefault(string url) =>
            Url.IsLocalUrl(url)? Redirect(url) : ReturnToDefault();
        private IActionResult ReturnToDefault() =>
            Redirect(Routes.Default);
    }
}
