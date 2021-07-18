using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Utils.Constants;

namespace WebApplication.Controllers
{
    [Authorize]
    public class AdminController: Controller
    {
        [HttpGet(Routes.Admin.Index)]
        public IActionResult Index()
        {
            return View();
        }
    }
}
