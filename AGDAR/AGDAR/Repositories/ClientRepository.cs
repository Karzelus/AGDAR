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
        public Client GetByEmail(string email)
        {
            return _dbContext.Clients.FirstOrDefault(c => c.Email == email);
        }
    }
}
