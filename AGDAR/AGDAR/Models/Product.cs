namespace AGDAR.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }   
        public double Price { get; set; }
        public string Description { get; set; }
        public string Brand { get; set; }
        public int StateId { get; set; }
        public virtual List<OrderProduct> Orders { get; set; }
        public virtual List<ProductCategory> Categorys { get; set; }
        public virtual List<PartProduct> Parts { get; set; }
    }
}