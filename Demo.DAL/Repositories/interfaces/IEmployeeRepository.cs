using Demo.DAL.Entities;

namespace Demo.DAL.Repositories.interfaces
{
    public interface IEmployeeRepository:IGenericRepository<Employee>
    {
        public IQueryable<Employee> SearchByName(string name);   
    }
}
