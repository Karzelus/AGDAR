using AGDAR.Models;
using Microsoft.EntityFrameworkCore;

namespace AGDAR.Repositories
{
    public class OrderHistoryRepository : BaseRepository<OrderHistory>
    {
        private AGDARDbContext _dbContext;
        public OrderHistoryRepository(AGDARDbContext dbcontext) : base(dbcontext)
        {
            _dbContext = dbcontext;
        }

    }
}
