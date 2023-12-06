using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace AGDAR.Models.DTOs
{
    public class CreateCustomProductDto
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="To pole nie może być puste")]
        [MaxLength(30)]
        public string Name { get; set; }
        [Required(ErrorMessage = "To pole nie może być puste")]
        public string Description { get; set; }
        public int StateId { get; set; }
        [Required(ErrorMessage = "Prosze wybrać typ narzędzia")]
        public int Type { get; set; }
        public double? Price { get; set; }
        public List<SelectListItem>? PartsList { get; set; }
        public string[]? PartsId { get; set; }
        public List<Part>? Parts { get; set; } = new List<Part>();
        public int ClientId { get; set; }
    }
}
