using System.ComponentModel.DataAnnotations;

namespace WebApplication.Models
{
    public class SignInViewModel
    {
        [Required(ErrorMessage = "This field is required")]
        [Display(Name = "Username")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool IsPersistent { get; set; }
    }
}