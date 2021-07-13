using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApplication.Data.Entities;


namespace WebApplication.Data
{
    public class ApplicationDbContext : IdentityDbContext<User,Role,Guid>
    {
        public ApplicationDbContext(DbContextOptions options) : base(options) { }
        
    }
}
