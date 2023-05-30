namespace AGDAR.Models
{
    public class OrderProduct
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public Order OrderMany { get; set; }
        public Product ProductMany { get; set; }
    }
}
