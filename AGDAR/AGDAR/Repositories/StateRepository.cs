using AGDAR.Models;

namespace AGDAR.Repositories
{
    public class StateRepository : BaseRepository<State>
    {
        private AGDARDbContext _dbContext;

        public StateRepository(AGDARDbContext dbcontext) : base(dbcontext)
        {
            _dbContext = dbcontext;
        }
    }
}
