using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace AGDAR.Models.DTO
{
    public class ProductDto
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="To pole nie może być puste")]
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
        public List<Category>? Categories { get; set; } = new List<Category>();
        [Required(ErrorMessage = "To pole nie może być puste")]
        public List<SelectListItem> CategoriesList { get; set; }
        [Required(ErrorMessage = "Produkt musi mieć przynajmniej jedą kategorię")]
        public string[] CategoriesId { get; set; }
        public List<PartProduct>? Parts { get; set; }
    }
}
