using AGDAR.Models;

namespace AGDAR.Repositories
{
    public class RoleRepository : BaseRepository<Role>
    {
        private AGDARDbContext _dbContext;

        public RoleRepository(AGDARDbContext dbcontext) : base(dbcontext)
        {
            _dbContext = dbcontext;
        }
    }
}
