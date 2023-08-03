namespace AGDAR.Models.DTO
{
    public class OrderDto
    {
        public int Id { get; set; }
        public List<OrderProduct>? Products { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        //public int ClientId { get; set; }
    }
}
