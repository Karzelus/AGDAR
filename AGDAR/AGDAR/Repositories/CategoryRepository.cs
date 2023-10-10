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
        public Category Find(string name)
        {
            return _dbContext.Categories.FirstOrDefault(x => x.Name == name);
        }
    }

}
