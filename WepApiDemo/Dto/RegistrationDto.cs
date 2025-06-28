using System.ComponentModel.DataAnnotations;

namespace WepApiDemo.Dto
{
    public class RegistrationDto
    {
        [Required]
        public string UserName { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
        [Required]
        [Compare("Password")]
        public string ConfirmPassword { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

    }
}
