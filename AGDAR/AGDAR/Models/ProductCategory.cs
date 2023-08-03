using System.ComponentModel.DataAnnotations.Schema;

namespace AGDAR.Models
{
    public class ProductCategory
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int CategoryId { get; set; }
        public Product ProductMany { get; set; }
        public Category CategoryMany { get; set; }
    }
}