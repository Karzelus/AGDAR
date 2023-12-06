using System.ComponentModel.DataAnnotations;

namespace AGDAR.Models.DTO
{
    public class ClientDto
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "To pole nie może być puste")]
        public string Name { get; set; }
        [Required(ErrorMessage = "To pole nie może być puste")]
        public string SeckondName { get; set; }
        [Required(ErrorMessage = "To pole nie może być puste")]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "To pole nie może być puste")]
        public string Password { get; set; }
        [Required(ErrorMessage = "To pole nie może być puste")]
        [Compare("Password", ErrorMessage = "Hasła nie są takie same")]
        public string ConfirmPassword { get; set; }
        [Required(ErrorMessage = "To pole nie może być puste")]
        public DateTime DateOfBirth { get; set; }
        public int? OrderdId { get; set; }
    }
}
