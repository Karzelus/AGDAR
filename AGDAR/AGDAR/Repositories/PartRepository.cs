using AGDAR.Models;

namespace AGDAR.Repositories
{
    public class PartRepository : BaseRepository<Part>
    {
        private AGDARDbContext _dbContext;

        public PartRepository(AGDARDbContext dbcontext) : base(dbcontext)
        {
            _dbContext = dbcontext;
        }
    }
}
