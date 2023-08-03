namespace AGDAR.Models.DTO
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public string Brand { get; set; }
        public List<ProductCategory> Categorys { get; set; }
        public List<PartProduct> Parts { get; set; }
    }
}
