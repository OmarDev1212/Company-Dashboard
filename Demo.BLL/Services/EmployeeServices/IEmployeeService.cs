using Demo.BLL.DTO.Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Services.EmployeeServices
{
    public interface IEmployeeService
    {
        public IEnumerable<EmployeeDto> GetAllEmployees();
        public IEnumerable<EmployeeDto> SearchEmployeesByName(string name);
        public EmployeeDetailsDto GetById(int id);
        public int CreateNewEmployee(CreatedEmployeeDto employee);
        public int UpdateEmployee(UpdateEmployeeDto employee);
        public bool DeleteEmployee(int id);
    }
}
