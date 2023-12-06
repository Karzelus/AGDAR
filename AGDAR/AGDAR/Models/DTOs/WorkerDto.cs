using System.ComponentModel.DataAnnotations;

namespace AGDAR.Models.DTO
{
    public class WorkerDto
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "To pole nie może byc puste")]
        public string Name { get; set; }
        [Required(ErrorMessage = "To pole nie może byc puste")]
        public string SeckondName { get; set; }
        [Required(ErrorMessage = "To pole nie może byc puste")]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "To pole nie może byc puste")]
        public DateTime? DateOfBirth { get; set; }
        [Required(ErrorMessage = "To pole nie może byc puste")]
        public string Password { get; set; }
        [Required(ErrorMessage = "To pole nie może byc puste")]
        [Compare("Password", ErrorMessage = "Hasła nie są takie same")]
        public string ConfirmPassword { get; set; }
        public int RoleId { get; set; }
        public string? Role { get; set; }
        public string? UnHashedPassword { get; set; }
    }
}
