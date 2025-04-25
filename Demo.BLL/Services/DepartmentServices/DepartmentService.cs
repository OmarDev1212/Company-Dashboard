using Demo.BLL.DTO.Department;
using Demo.BLL.Mapping;
using Demo.DAL.Repositories.interfaces;
using Demo.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Services.DepartmentServices
{
    public class DepartmentService(/*IDepartmentRepository _departmentRepository*/ IUnitOfWork _unitOfWork) : IDepartmentService
    {
            
        public int AddDepartment(AddDepartmentDto department)
        {
             _unitOfWork.DepartmentRepository.AddEntity(department.ToEntity());
            return _unitOfWork.SaveChanges();
        }

        public IEnumerable<DepartmentDto> GetAllDepartments()
        {
            var departments = _unitOfWork.DepartmentRepository.GetAll();

            return departments.Select(d=>d.ToDepartmentDto()).ToList();
        }

        public DepartmentDetailsDto GetDepartmentById(int id)
        {
            var department= _unitOfWork.DepartmentRepository.GetEntityById(id);
            //if (department == null) return null;
            //return department.ToDepartmentDetailsDto();
            return department?.ToDepartmentDetailsDto();
        }
        public int UpdateDepartment(UpdateDepartmentDto department)
        {
           _unitOfWork.DepartmentRepository.UpdateEntity(department.ToEntity());
            return _unitOfWork.SaveChanges();
        }
        public bool DeleteDepartment(int id)
        {
            var department = _unitOfWork.DepartmentRepository.GetEntityById(id);
            if (department is null) return false;
             _unitOfWork.DepartmentRepository.DeleteEntity(department);
            int res = _unitOfWork.SaveChanges();
                return res > 0;
            
        }       
    }
}
