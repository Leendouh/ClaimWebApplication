using System.ComponentModel.DataAnnotations;

namespace ClaimWebApplication.ViewModel
{
    public class LoginViewModel
    {
        // validation for domain model
        [Display(Name = "Email Address")]
        [Required(ErrorMessage = "Email address is required")]
        public string EmailAddress { get; set; }

        [Required]
        // validation for domain model
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
