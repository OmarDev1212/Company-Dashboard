using Demo.BLL.DTO.Department;
using Demo.BLL.Mapping;
using Demo.BLL.Repositories.interfaces;
using Demo.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Services.DepartmentServices
{
    public class DepartmentService(IDepartmentRepository _departmentRepository) : IDepartmentService
    {

        public int AddDepartment(AddDepartmentDto department)
        {
            return _departmentRepository.AddEntity(department.ToEntity());
        }

        public IEnumerable<DepartmentDto> GetAllDepartments()
        {
            var departments = _departmentRepository.GetAll();

            return departments.Select(d=>d.ToDepartmentDto()).ToList();
        }

        public DepartmentDetailsDto GetDepartmentById(int id)
        {
            var department=_departmentRepository.GetEntityById(id);
            //if (department == null) return null;
            //return department.ToDepartmentDetailsDto();
            return department==null?null: department.ToDepartmentDetailsDto();
        }
        public int UpdateDepartment(UpdateDepartmentDto department)
        {
          return  _departmentRepository.UpdateEntity(department.ToEntity());
        }
        public bool DeleteDepartment(int id)
        {
            var department = _departmentRepository.GetEntityById(id);
            if (department is null) return false;
            else
            {
                int res=_departmentRepository.DeleteEntity(department);
                return res>0?true:false;
            }
        }       
    }
}
