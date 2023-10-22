using Microsoft.AspNetCore.Mvc.Rendering;

namespace AGDAR.Models.DTO
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public string Brand { get; set; }
        public int StateId { get; set; }
        public string Img { get; set; }
        public List<Category>? CategoriesList { get; set; } = new List<Category>();
        public List<PartProduct>? Parts { get; set; }
    }
}
