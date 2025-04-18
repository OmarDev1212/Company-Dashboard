using Demo.BLL.Repositories.interfaces;
using Demo.DAL.Data;
using Demo.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Repositories.repos
{
    public class GenericRepository<T>(ApplicationDbContext _dbContext) : IGenericRepository<T> where T : BaseEntity
    {
        public int AddEntity(T TEntity)
        {
            _dbContext.Add(TEntity);
            return _dbContext.SaveChanges();
        }

        public int DeleteEntity(T TEntity)
        {
            _dbContext.Remove(TEntity);
            return _dbContext.SaveChanges();
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

        public int UpdateEntity(T entity)
        {
            _dbContext.Update(entity);
            return _dbContext.SaveChanges();
        }
    }
}
