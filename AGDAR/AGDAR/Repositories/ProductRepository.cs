using AGDAR.Models;
using AGDAR.Models.DTO;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AGDAR.Repositories
{
    public class ProductRepository : BaseRepository<Product>
    {
        private AGDARDbContext _dbContext;

        public ProductRepository(AGDARDbContext dbcontext) : base(dbcontext)
        {
            _dbContext = dbcontext;
        }
        public IQueryable<Product> Include(params Expression<Func<Product, object>>[] includes)
        {
            IQueryable<Product> query = _dbContext.Products;

            if (includes != null)
            {
                query = includes.Aggregate(query, (current, include) => current.Include(include));
            }

            return query;
        }
    }
}
