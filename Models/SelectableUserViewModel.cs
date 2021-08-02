using System;

namespace WebApplication.Models
{
    public class SelectableUserViewModel
    {
        public string UserName { get; set; }

        public string Email { get; set; }

        public Guid Id { get; set; }

        public DateTime RegisteredAt { get; set; }

        public DateTime LastAuthorizedAt { get; set; }

        public bool IsBaned { get; set; }

        public bool IsSelected { get; set; }

    }
}
