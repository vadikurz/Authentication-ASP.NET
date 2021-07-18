using System;
using Microsoft.AspNetCore.Identity;

namespace WebApplication.Data.Entities
{
    public class User : IdentityUser<Guid> 
    {
        public DateTime RegistrationDate { get; set; }

        public DateTime LastAuthorization { get; set; }
    }
}
