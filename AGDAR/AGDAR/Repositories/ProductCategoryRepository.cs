using AGDAR.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AGDAR.Repositories
{
    public class ProductCategoryRepository : BaseRepository<ProductCategory>
    {
        private AGDARDbContext _dbContext;

        public ProductCategoryRepository(AGDARDbContext dbcontext) : base(dbcontext)
        {
            _dbContext = dbcontext;
        }
    }
}
