using System.ComponentModel.DataAnnotations;

namespace AGDAR.Models.DTO
{
    public class CreateProductDto
    {
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }
        [Required]
        public double Price { get; set; }
        public string Description { get; set; }
        public string Brand { get; set; }
        public List<ProductCategory>? Categorys { get; set; }
        public List<PartProduct>? Parts { get; set; }
    }
}
