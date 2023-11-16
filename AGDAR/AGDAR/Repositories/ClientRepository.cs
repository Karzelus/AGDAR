using AGDAR.Models;
using Microsoft.EntityFrameworkCore;

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
        public void AddOrderId(int id, int orderId)
        {
            Client clientToUpdate = _dbContext.Clients.FirstOrDefault(x => x.Id == id);
            if (clientToUpdate != null)
            {
                clientToUpdate.OrderdId = orderId;
                _dbContext.SaveChanges();
            }
        }
    }
}
