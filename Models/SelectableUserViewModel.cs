using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Data.Entities;

namespace WebApplication.Models
{
    public class SelectableUserViewModel
    {
        public string UserName { get; set; }
        public string Email { get; set; }

        public Guid Id { get; set; }
        public DateTime RegistrationDate { get; set; }

        public DateTime LastAuthorization { get; set; }
        public bool IsSelected { get; set; }

    }
}
