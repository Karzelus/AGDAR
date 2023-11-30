using Microsoft.AspNetCore.Mvc.Rendering;

namespace AGDAR.Models.DTO
{
    public class OrderDto
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public List<Product>? Products { get; set; } = new List<Product>();
        public List<SelectListItem> ProductsList { get; set; }
        public int? ClientId { get; set; }
        public string? ClientName { get; set; }
        public string[] ProductsId { get; set; }
    }
}
