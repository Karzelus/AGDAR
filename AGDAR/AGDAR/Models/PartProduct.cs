namespace AGDAR.Models
{
    public class PartProduct
    {
        public int Id { get; set; }
        public int PartId { get; set; }
        public int ProductId { get; set; }
        public Part PartMany { get; set; }
        public Product ProductMany { get; set; }
    }
}
