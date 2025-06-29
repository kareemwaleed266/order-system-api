using System.ComponentModel.DataAnnotations;

namespace Order.Service.Services.UserService.Dto
{
    public class RegisterDto
    {
        [Required]
        public string DisplayName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d).*$", ErrorMessage = "Error: Password must contain at least one uppercase letter, one lowercase letter, and one digit.")]
        public string Password { get; set; }
    }
}
