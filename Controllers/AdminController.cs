using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using WebApplication.Utils.Constants;

namespace WebApplication.Controllers
{
    
    [Authorize]
    public class AdminController: Controller
    {
        [Route(Routes.Admin.Index)]
        public IActionResult Index()
        {
            return View();
        }
    }
}
