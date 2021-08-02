using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Data.Entities;
using WebApplication.Mappers;
using WebApplication.Models;
using WebApplication.Utils.Constants;
using WebApplication.Utils.Extensions;


namespace WebApplication.Controllers
{
    [Authorize]
    public class AdminController: Controller
    {
        private readonly UserManager<User> userManager;
        private readonly IMapper<User, SelectableUserViewModel> mapper;
        
        public AdminController(UserManager<User> userManager, IMapper<User,SelectableUserViewModel> mapper)
        {
            this.userManager = userManager;
            this.mapper = mapper;
        }

        [HttpGet(Routes.Admin.Index)]
        public IActionResult Index()
        {
            return View(userManager.Users.AsEnumerable().Select(mapper.Map).ToList());
        }

        [HttpPost(Routes.Admin.Delete)]
        public IActionResult DeleteUsersAsync(IEnumerable<SelectableUserViewModel> users)
        {
            users.Where(user => user.IsSelected)
                 .Select(user=>user.Id)
                 .Select(GetUserById)
                 .NotNull()
                 .ForEach(DeleteUser);

             return Redirect(Routes.Admin.Index);
        }

        private User GetUserById(Guid id) => userManager.FindByIdAsync(id.ToString()).Result;

        private void DeleteUser(User user) => userManager.DeleteAsync(user).Wait();
    }
   
}
