namespace AGDAR.Models.DTO
{
    public class CreateOrderDto
    {
        public string Description { get; set; }
        public double Price { get; set; }
        public int ClientId { get; set; }
    }
}
