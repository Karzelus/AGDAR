using AGDAR.Models;
using AGDAR.Models.DTO;
using Microsoft.EntityFrameworkCore;

namespace AGDAR.Repositories
{
    public class ProductRepository : BaseRepository<Product>
    {
        private AGDARDbContext _dbContext;

        public ProductRepository(AGDARDbContext dbcontext) : base(dbcontext)
        {
            _dbContext = dbcontext;
        }
    }
}
