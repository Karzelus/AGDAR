namespace AGDAR.Models
{
    public class OrderHistory
    {
        public int Id { get; set; }
        public int? ClientId { get; set; }
        public string ClientName { get; set; }
        public double Price { get; set; }
        public DateTime OrderEndDate { get; set; }
        public int OrderId { get; set; }
    }
}
