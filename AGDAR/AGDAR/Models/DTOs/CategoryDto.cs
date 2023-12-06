using System.ComponentModel.DataAnnotations;

namespace AGDAR.Models.DTO
{
    public class CategoryDto
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "To pole nie może być puste")]
        public string Name { get; set; }
    }
}
