using AGDAR.Models;

namespace AGDAR.Repositories
{
    public class OrderRepository : BaseRepository<Order>
    {
        private AGDARDbContext _dbContext;

        public OrderRepository(AGDARDbContext dbcontext) : base(dbcontext)
        {
            _dbContext = dbcontext;
        }
    }
}
