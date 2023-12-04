using AGDAR.Models;

namespace AGDAR.Repositories
{
    public class NewsletterEmailsRepository : BaseRepository<NewsletterEmails>
    {
        private AGDARDbContext _dbContext;

        public NewsletterEmailsRepository(AGDARDbContext dbcontext) : base(dbcontext)
        {
            _dbContext = dbcontext;
        }
    }
}
