using AutoMapper;
using Demo.BLL.DTO.Employee;
using Demo.BLL.Repositories.interfaces;
using Demo.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Services.EmployeeServices
{
    public class EmployeeService(IEmployeeRepository _employeeRepository, IMapper _mapper) : IEmployeeService
    {
        public int CreateNewEmployee(CreatedEmployeeDto employee)
        {
            var mappedEmployee = _mapper.Map<CreatedEmployeeDto, Employee>(employee);
            return _employeeRepository.AddEntity(mappedEmployee);
        }

        public bool DeleteEmployee(int id)//soft delete
        {
            var employee = _employeeRepository.GetEntityById(id);
            if(employee == null) return false;
            employee.IsDeleted=true;
            return _employeeRepository.UpdateEntity(employee)>0;
        }

        public IEnumerable<EmployeeDto> GetAllEmployees()
        {
            var employees = _employeeRepository.GetAll();
            return _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeDto>>(employees);

        }

        public EmployeeDetailsDto GetById(int id)
        {
            var employee = _employeeRepository.GetEntityById(id);
            if (employee == null) return null;
            return _mapper.Map<Employee, EmployeeDetailsDto>(employee);
        }

        public int UpdateEmployee(UpdateEmployeeDto employee)
        {
            var mappedEmp = _mapper.Map<UpdateEmployeeDto, Employee>(employee);
            return _employeeRepository.UpdateEntity(mappedEmp);
        }
    }
}
