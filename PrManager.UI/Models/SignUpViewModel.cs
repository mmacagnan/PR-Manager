using System.ComponentModel.DataAnnotations;

namespace PrManager.UI.Models
{
    public class SignUpViewModel
    {
        [Required(ErrorMessage = "Please, tell us your name")]
        [MaxLength(80, ErrorMessage = "This field cannot be bigger than {0} chars")]
        public string FirstName { get; set; }
        
        [Required(ErrorMessage = "Please, tell us your surname")]
        [MaxLength(80, ErrorMessage = "This field cannot be bigger than {0} chars")]
        public string LastName { get; set; }
        
        [Required(ErrorMessage = "Fill your email")]
        [EmailAddress(ErrorMessage = "Please, put a valid email address")]
        public string Email { get; set; }
        
        [Required(ErrorMessage = "Create a username")]
        [MaxLength(16, ErrorMessage = "Your username cannot be bigger than {0} chars")]
        public string UserName { get; set; }
        
        [Required(ErrorMessage = "Fill your password")]
        [StringLength(16, ErrorMessage = "Your password must be {0} to {2} chars long", MinimumLength = 8)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        
        [Compare("Password", ErrorMessage = "The password doesn't match")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}