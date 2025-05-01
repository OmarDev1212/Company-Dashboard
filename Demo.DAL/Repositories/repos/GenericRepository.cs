using Demo.DAL.Data;
using Demo.DAL.Entities;
using Demo.DAL.Repositories.interfaces;
using Microsoft.EntityFrameworkCore;

namespace Demo.DAL.Repositories.repos
{
    public class GenericRepository<T>(ApplicationDbContext _dbContext) : IGenericRepository<T> where T : BaseEntity
    {
        public void AddEntity(T TEntity)
        {
            _dbContext.Add(TEntity);
        }

        public void DeleteEntity(T TEntity)
        {
            _dbContext.Remove(TEntity);
        }

        public IEnumerable<T> GetAll(bool withTracking = false)
        {
            if (withTracking)
                return _dbContext.Set<T>().Where(e => e.IsDeleted != true).ToList();
            else
                return _dbContext.Set<T>().AsNoTracking().Where(t => t.IsDeleted != true).ToList();
        }

        public T GetEntityById(int id)
        {
            return _dbContext.Set<T>().Find(id);
        }

        public void UpdateEntity(T entity)
        {
            _dbContext.Update(entity);
        }
    }
}
