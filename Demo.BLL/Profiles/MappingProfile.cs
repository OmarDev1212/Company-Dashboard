using AutoMapper;
using Demo.BLL.DTO.Employee;
using Demo.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreatedEmployeeDto, Employee>()
                .ForMember(e => e.EmployeeGender, g => g.MapFrom(g => g.EmployeeGender))
                .ForMember(e => e.EmployeeType, t => t.MapFrom(t => t.EmployeeType));

            CreateMap<UpdateEmployeeDto, Employee>()
                .ForMember(e => e.EmployeeGender, g => g.MapFrom(g => g.EmployeeGender))
                .ForMember(e => e.EmployeeType, t => t.MapFrom(t => t.EmployeeType))
                .ForMember(e=>e.HireDate,dest=>dest.MapFrom(dest=>dest.HireDate));

            CreateMap<Employee, EmployeeDto>()
                .ForMember(e => e.EmployeeGender, g => g.MapFrom(g => g.EmployeeGender))
                .ForMember(e => e.EmployeeType, t => t.MapFrom(t => t.EmployeeType));

            CreateMap<Employee, EmployeeDetailsDto>()
                .ForMember(e => e.EmployeeGender, g => g.MapFrom(g => g.EmployeeGender))
                .ForMember(e => e.EmployeeType, t => t.MapFrom(t => t.EmployeeType));
        }
    }
}
