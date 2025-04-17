using Demo.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Repositories.interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        public IEnumerable<TEntity> GetAll(bool withTracking = false); // IEnumerable to iterate on rows to put them in view  also gets all data from database and load it in memory
                                                                                     //IQueryable used to filter data in database not in application direct
                                                                                     //ICollection used for need for Opertions like add,update,delete
                                                                                    // IReadOnlyList used only for read and no need for iteration used in API as returned data is json
        public TEntity GetEntityById(int id);
        public int UpdateEntity(TEntity entity); //int to return no of affected rows in database
        public int AddEntity(TEntity entity);
        public int DeleteEntity(TEntity entity);
    }
}
