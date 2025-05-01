using Demo.DAL.Data;
using Demo.DAL.Entities;
using Demo.DAL.Repositories.interfaces;

namespace Demo.DAL.Repositories.repos
{
    public class EmployeeRepository(ApplicationDbContext _dbContext) : GenericRepository<Employee>(_dbContext), IEmployeeRepository
    {
        public IQueryable<Employee> SearchByName(string name)
        {
            return _dbContext.Employees.Where(e => e.Name.ToLower().Contains(name.ToLower()));
        }
    }
}
