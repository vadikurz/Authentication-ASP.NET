using System.ComponentModel.DataAnnotations;

namespace WebApplication.Models
{
    public class SignUpViewModel
    {
        [Required(ErrorMessage = "This field is required")]
        [Display(Name = "Username")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare(nameof(Password))]
        public string ConfirmedPassword { get; set; }

        [Display(Name = "Remember me?")]
        public bool IsPersistent { get; set; }
    }
}
