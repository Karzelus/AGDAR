using AGDAR.Models;

namespace AGDAR.Repositories
{
    public class ClientRepository : BaseRepository<Client>
    {
        private AGDARDbContext _dbContext;

        public ClientRepository(AGDARDbContext dbcontext) : base(dbcontext)
        {
            _dbContext = dbcontext;
        }
    }
}
