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

            if (model.Password != model.ConfirmedPassword)
            {
                ModelState.AddModelError("Password", "incorrectly");
                return View(model);
            }

            User user = new User
            {
                UserName = model.UserName,
                RegistrationDate = DateTime.Now
            };
            var result = userManager.CreateAsync(user, model.Password).GetAwaiter().GetResult();
            if (result.Succeeded)
            {
                await signInManager.SignInAsync(user, false);
                return Redirect(returnUrl);
            }
            return View(model);
        }

        [HttpGet(Routes.Account.SignIn)]
        public IActionResult Signin()
        {
            return View();
        }

        [HttpPost(Routes.Account.SignIn)]
        public async Task<IActionResult> SignInAsync([FromForm]LoginViewModel model, [FromQuery(Name = ApplicationCookies.ReturnUrlParameter)] string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await userManager.FindByNameAsync(model.UserName);
            if (user == null)
            {
                ModelState.AddModelError("","username not found");
                return View(model);
            }

            var result = await signInManager.PasswordSignInAsync(user,model.Password,false,false);
            if (result.Succeeded)
            {
                return redirectToUrlOrDefault(returnUrl);
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

        private IActionResult redirectToUrlOrDefault(string url) =>
            IsValidUrl(url) ? Redirect(url) : ReturnToDefault();

        private bool IsValidUrl(string url) => Url.IsLocalUrl(url);

        private IActionResult ReturnToDefault() =>
            Redirect(Routes.Default);
    }
}
