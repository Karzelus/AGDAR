using AGDAR.Models;

namespace AGDAR.Repositories
{
    public class OrderProductRepository : BaseRepository<OrderProduct>
    {
        private AGDARDbContext _dbContext;

        public OrderProductRepository(AGDARDbContext dbcontext) : base(dbcontext)
        {
            _dbContext = dbcontext;
        }
    }
}
