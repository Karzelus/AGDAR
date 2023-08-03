
using AGDAR.Models;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace AGDAR.Repositories
{
    public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class, new()
    {
        private readonly AGDARDbContext _dbContext;
        protected BaseRepository(AGDARDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        protected AGDARDbContext AGDARDbContext
        {
            get { return _dbContext; }
        }

        public virtual TEntity Add(TEntity entity)
        {
            _dbContext.AddEntity(entity);

            return entity;
        }

        public virtual TEntity AddAndSaveChanges(TEntity entity) 
        {
            _dbContext.AddEntityAndSaveChanges(entity); 
            return entity;
        }

        public virtual void AddRange(IEnumerable<TEntity> entity) 
        {
            _dbContext.AddEntitiesRange(entity);
        }

        public virtual void AddRangeAndSaveChanges(IEnumerable<TEntity> entity) 
        {
            _dbContext.AddEntitiesRangeAndSaveChanges(entity);
        }

        public virtual void Attach(TEntity entity)
        {
            _dbContext.Set<TEntity>().Attach(entity);
        }

        public virtual void Update(TEntity entity)
        {
            _dbContext.UpdateEntity(entity);
        }

        public virtual void UpdateAndSaveChanges(TEntity entity)
        {
            Update(entity);
            _dbContext.SaveChanges();
        }

        public virtual void UpdateRangeAndSaveChanges(IEnumerable<TEntity> entities)
        {
            _dbContext.UpdateEntitiesRangeAndSaveChanges(entities);
        }

        public virtual void Remove(TEntity entity)
        {
            _dbContext.RemoveEntity(entity);
        }

        public virtual void RemoveById(int id)
        {
            var entity = GetById(id);
            if (entity != null)
            {
                Remove(entity);
            }
        }

        public virtual void RemoveByIdAndSaveChanges(int id)
        {
            RemoveById(id);
            SaveChanges();
        }

        public virtual void RemoveRange(IEnumerable<TEntity> entity)
        {
            _dbContext.RemoveEntitiesRange(entity);
        }

        public virtual void RemoveRangeAndSaveChanges(IEnumerable<TEntity> entity)
        {
            _dbContext.RemoveEntitiesRangeAndSaveChanges(entity);
        }

        public virtual void DetectedChanges()
        {
            _dbContext.ChangeTracker.DetectChanges();
        }

        public virtual void SaveChanges()
        {
            _dbContext.SaveChanges();
        }

        public virtual IQueryable<TEntity> GetAll()
        {
            return AGDARDbContext.Set<TEntity>();
        }

        public void Dispose()
        {
            if (_dbContext != null)
            {
                _dbContext.Dispose();

            }
        }

        public virtual TEntity GetById(int id)
        {
            return AGDARDbContext.Set<TEntity>().Find(id);
        }

    }
}
