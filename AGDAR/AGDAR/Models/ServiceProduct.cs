namespace AGDAR.Models
{
    public class ServiceProduct
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public string Name { get; set; }
        public string ClientNote { get; set; }
        public string? ClientEmail { get; set; }
        public int? WorkerId { get; set; }
        public string? WorkerName { get; set; }
        public string? WorkerNote { get; set; }
        public double? Price { get; set; }
        public int? Status { get; set; }
    }
}
