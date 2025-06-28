using System.ComponentModel.DataAnnotations;

namespace WepApiDemo.Dto
{
    public class LogInDto
    {
        [Required]
        public string Name {  get; set; }
        [Required]

        public string Password { get; set; }
    }
}
