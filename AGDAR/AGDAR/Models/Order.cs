namespace AGDAR.Models
{
    public class Order
    {
        public int Id { get; set; }
        public List<OrderProduct> Products { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public int ClientId { get; set; }
    }
}
