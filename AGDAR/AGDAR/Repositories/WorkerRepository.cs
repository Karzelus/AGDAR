using AGDAR.Models;

namespace AGDAR.Repositories
{
    public class WorkerRepository : BaseRepository<Worker>
    {
        private AGDARDbContext _dbContext;

        public WorkerRepository(AGDARDbContext dbcontext) : base(dbcontext)
        {
            _dbContext = dbcontext;
        }
        public Worker GetByEmail(string email)
        {
            return _dbContext.Workers.FirstOrDefault(w => w.Email == email);
        }
    }
}
