using System;
using Microsoft.AspNetCore.Identity;

namespace WebApplication.Data.Entities
{
    public class User : IdentityUser<Guid>
    {
        public bool IsBanned { get; set; }

        public DateTime RegisteredAt { get; set; }

        public DateTime LastAuthorizedAt { get; set; }
    }
}
