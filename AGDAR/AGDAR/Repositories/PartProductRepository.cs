using AGDAR.Models;

namespace AGDAR.Repositories
{
    public class PartProductRepository : BaseRepository<PartProduct>
    {
        private AGDARDbContext _dbContext;

        public PartProductRepository(AGDARDbContext dbcontext) : base(dbcontext)
        {
            _dbContext = dbcontext;
        }
    }
}
