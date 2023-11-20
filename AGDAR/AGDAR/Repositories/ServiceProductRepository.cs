using AGDAR.Models;
using Microsoft.EntityFrameworkCore;
namespace AGDAR.Repositories
{
    public class ServiceProductRepository : BaseRepository<ServiceProduct>
    {
        private AGDARDbContext _dbContext;

        public ServiceProductRepository(AGDARDbContext dbcontext) : base(dbcontext)
        {
            _dbContext = dbcontext;
        }
    }
}
