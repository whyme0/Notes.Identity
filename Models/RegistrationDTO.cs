using System.ComponentModel.DataAnnotations;

namespace Notes.Identity.Models
{
    public class RegistrationDTO
    {
        [Required(ErrorMessage = "This field is required")]
        public string Username { get; set; }
        
        [Required(ErrorMessage = "This field is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords don't match")]
        public string ConfirmPassword { get; set; }

        public string ReturnUrl { get; set; }
    }
}
