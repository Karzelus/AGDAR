using System.ComponentModel.DataAnnotations;

namespace AGDAR.Models.DTOs
{
    public class LoginDto
    {
        [Required(ErrorMessage = "To pole nie może byc puste")]
        public string Email { get; set; }
        [Required(ErrorMessage = "To pole nie może byc puste")]
        public string Password { get; set; }
    }
}
