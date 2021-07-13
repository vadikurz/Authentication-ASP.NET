using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace WebApplication.Data.Entities
{
    public class Role: IdentityRole<Guid>
    {
    }
}
