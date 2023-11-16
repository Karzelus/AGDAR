using AGDAR.Models;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Newtonsoft.Json.Linq;

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
