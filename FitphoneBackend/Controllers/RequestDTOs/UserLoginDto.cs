using System.ComponentModel.DataAnnotations;
namespace FitphoneBackend.Controllers.DTOs{
    public class UserLoginDto{
        [Required]
        public string Email { get; private set; }

        [Required]
        public string Password { get; private set; } 
    }
}