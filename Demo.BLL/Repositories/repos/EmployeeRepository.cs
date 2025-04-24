using Demo.BLL.Repositories.interfaces;
using Demo.DAL.Data;
using Demo.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Repositories.repos
{
    public class EmployeeRepository(ApplicationDbContext _dbContext) : GenericRepository<Employee>(_dbContext), IEmployeeRepository
    {
        public IQueryable<Employee> SearchByName(string name)
        {
            return _dbContext.Employees.Where(e => e.Name.ToLower().Contains(name.ToLower()));
        }
    }
}
