using System.ComponentModel.DataAnnotations;

namespace AGDAR.Models.DTO
{
    public class CreateProductDto
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }
        [Required]
        public double Price { get; set; }
        public string Description { get; set; }
        public string Brand { get; set; }
        public int StateId { get; set; }
        public List<Category>? Categories { get; set; }
        public List<PartProduct>? Parts { get; set; }
    }
}
