using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace AGDAR.Models.DTO
{
    public class CreateProductDto
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "To pole nie może być puste")]
        [MaxLength(30)]
        public string Name { get; set; }
        [Required(ErrorMessage = "To pole nie może być puste")]
        public double Price { get; set; }
        [Required(ErrorMessage = "To pole nie może być puste")]
        public string Description { get; set; }
        [Required(ErrorMessage = "To pole nie może być puste")]
        public string Brand { get; set; }
        public int StateId { get; set; }
        [Required(ErrorMessage = "To pole nie może być puste")]
        public string Img { get; set; }
        public List<SelectListItem>? CategoriesList { get; set; }
        public string[]? CategoriesId { get; set; }
        public int? Type { get; set; }
    }
}
