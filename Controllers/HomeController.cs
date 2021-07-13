using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
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
