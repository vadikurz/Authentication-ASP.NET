using System.ComponentModel.DataAnnotations;

namespace WebApplication.Models
{
    public class SignInViewModel
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare(nameof(Password))]
        public string Password { get; set; }
    }
}