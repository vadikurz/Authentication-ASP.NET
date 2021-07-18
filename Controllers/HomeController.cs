using Microsoft.AspNetCore.Mvc;
using WebApplication.Utils.Constants;

namespace WebApplication.Controllers
{
    public class HomeController : Controller
    {
        [Route(Routes.Default)]
        [Route(Routes.Home.Index)]
        public IActionResult Index()
        {
            return View();
        }
    }
}
