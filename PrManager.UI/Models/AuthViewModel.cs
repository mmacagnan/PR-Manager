using System.ComponentModel.DataAnnotations;

namespace PrManager.UI.Models
{
    public class AuthViewModel
    {
        [Required(ErrorMessage = "Please, fill the login info")]
        [MaxLength(16, ErrorMessage = "The username cannot be bigger than {0} chars")]
        public string UserName { get; set; }
        
        [Required(ErrorMessage = "Please, input password")]
        [StringLength(16, ErrorMessage = "The password field must contains {0} to {2} chars", MinimumLength = 8)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}