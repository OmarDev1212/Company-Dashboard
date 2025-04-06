using Demo.BLL.DTO.Department;
using Demo.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Mapping
{
    public static class MappingDepartmentExtensions
    {
        //        Get All Departments :
        //Return Id, Name, Code, Description and Date Of Creation Only

        public static DepartmentDto ToDepartmentDto(this Department department)
        {
            var DepartmentDtoToReturn = new DepartmentDto()
            {
                Id = department.Id,
                Code = department.Code,
                DateOfCreation = DateOnly.FromDateTime(department.CreatedOn),
                Description = department.Description,
                Name = department.Name,

            };
            return DepartmentDtoToReturn;
        }
        public static DepartmentDetailsDto ToDepartmentDetailsDto(this Department department)
        {
            var DepartmentDtoToReturn = new DepartmentDetailsDto()
            {
                Id = department.Id,
                Code = department.Code,
                DateOfCreation = DateOnly.FromDateTime(department.CreatedOn),
                Description = department.Description,
                Name = department.Name,
                CreatedBy = department.CreatedBy,
                IsDeleted = department.IsDeleted,
                ModifiedBy = department.LastModifiedBy,
                LastModifiedOn= DateOnly.FromDateTime(department.LastModifiedOn.Value)
                
            };
            return DepartmentDtoToReturn;
        }
        
        public static Department ToEntity(this AddDepartmentDto department)
        {

            var departmentToreturn = new Department()
            {
                Name = department.Name,
                Code = department.Code,
                Description = department.Description,
                CreatedOn = department.DateOfCreation.ToDateTime(TimeOnly.MinValue),
                CreatedBy=1,
                LastModifiedBy=1
            };
            return departmentToreturn;
        }
        public static Department ToEntity(this UpdateDepartmentDto department)
        {

            var departmentToreturn = new Department()
            {
                Id = department.Id, 
                Name = department.Name,
                Code = department.Code,
                Description = department.Description,
                CreatedOn = department.DateOfCreation.ToDateTime(TimeOnly.MinValue),
                LastModifiedBy = 1
            };
            return departmentToreturn;
        }
    }
}
