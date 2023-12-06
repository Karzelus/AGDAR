using System.ComponentModel.DataAnnotations;

namespace AGDAR.Models.DTO
{
    public class PartDto
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="To pole nie może być puste")]
        public string Name { get; set; }
        [Required(ErrorMessage = "To pole nie może być puste")]
        public string Description { get; set; }
        [Required(ErrorMessage = "To pole nie może być puste")]
        [Range(1, 10000)]
        public double Price { get; set; }
        public int Type { get; set; }
        public int ToolType { get; set; }
    }
}
