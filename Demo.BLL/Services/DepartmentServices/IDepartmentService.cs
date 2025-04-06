using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.BLL.DTO.Department;
using Demo.DAL.Entities;

namespace Demo.BLL.Services.DepartmentServices
{
    public interface IDepartmentService
    {
        public IEnumerable<DepartmentDto> GetAllDepartments();
        public DepartmentDetailsDto GetDepartmentById(int id);
        public int AddDepartment(AddDepartmentDto department);
        public int UpdateDepartment(UpdateDepartmentDto department);

        public bool DeleteDepartment(int id);
    }
}
