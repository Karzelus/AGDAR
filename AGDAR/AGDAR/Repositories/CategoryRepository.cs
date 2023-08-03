using AGDAR.Models;

namespace AGDAR.Repositories
{
    public class CategoryRepository : BaseRepository<Category>
    {
        private AGDARDbContext _dbContext;

        public CategoryRepository(AGDARDbContext dbcontext) : base(dbcontext) 
        {
            _dbContext = dbcontext;
        }
    }
}
