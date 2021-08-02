using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Data.Entities;
using WebApplication.Mappers;
using WebApplication.Utils.Constants;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    public class AccountController : Controller
    {
        private const string UserNotFoundMessage = "Username is not found";
        private const string UserBannedMessage = "User is banned"; 
        private const string IncorrectUserNameOrPassword = " Incorrect username or password";


        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly IMapper<SignUpViewModel, User> mapper;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, IMapper<SignUpViewModel, User> mapper)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.mapper = mapper;
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
        public async Task<IActionResult> SignUpAsync([FromForm] SignUpViewModel model, [FromQuery(Name = ApplicationCookies.ReturnUrlParameter)] string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var user = mapper.Map(model);
                user.RegisteredAt = DateTime.Now;
                user.LastAuthorizedAt = DateTime.Now;
                var result = await userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await signInManager.SignInAsync(user, model.IsPersistent);
                    return RedirectToUrlOrDefault(returnUrl);
                }
            }
            return View(model);
        }

        [HttpGet(Routes.Account.SignIn)]
        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost(Routes.Account.SignIn)]
        public async Task<IActionResult> SignInAsync([FromForm] SignInViewModel model, [FromQuery(Name = ApplicationCookies.ReturnUrlParameter)] string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await userManager.FindByNameAsync(model.UserName);
            if (user is null)
            {
                ModelState.AddModelError(string.Empty, UserNotFoundMessage);
                return View(model);
            }

            if (user.IsBanned)
            {
                ModelState.AddModelError(string.Empty, UserBannedMessage);
                return View(model);
            }

            var result = await signInManager.PasswordSignInAsync(user, model.Password, model.IsPersistent, lockoutOnFailure: false);
            if (!result.Succeeded)
            {
                ModelState.AddModelError(string.Empty, IncorrectUserNameOrPassword);
                return View(model);
            }

            user.LastAuthorizedAt = DateTime.Now;
            await userManager.UpdateAsync(user);
            return RedirectToUrlOrDefault(returnUrl);

        }

        [Authorize]
        [HttpGet(Routes.Account.SignOut)]
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

        private IActionResult RedirectToUrlOrDefault(string url) => Url.IsLocalUrl(url) ? Redirect(url) : ReturnToDefault();

        private IActionResult ReturnToDefault() => Redirect(Routes.Default);
    }
}
