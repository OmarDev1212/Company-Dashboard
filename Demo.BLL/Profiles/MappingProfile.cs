using AutoMapper;
using Demo.BLL.DTO.Employee;
using Demo.DAL.Entities;

namespace Demo.BLL.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreatedEmployeeDto, Employee>()
                .ForMember(dest => dest.EmployeeGender, g => g.MapFrom(g => g.EmployeeGender))
                .ForMember(dest => dest.EmployeeType, t => t.MapFrom(t => t.EmployeeType));

            CreateMap<UpdateEmployeeDto, Employee>()
                .ForMember(e => e.EmployeeGender, g => g.MapFrom(g => g.EmployeeGender))
                .ForMember(e => e.EmployeeType, t => t.MapFrom(t => t.EmployeeType))
                .ForMember(e => e.HireDate, dest => dest.MapFrom(dest => dest.HireDate))
                .ForMember(e => e.ImageName, dest => dest.MapFrom(e => e.Image.FileName ?? "NotFound"));

            CreateMap<Employee, EmployeeDto>()
                .ForMember(dest => dest.EmployeeGender, g => g.MapFrom(g => g.EmployeeGender))
                .ForMember(dest => dest.EmployeeType, t => t.MapFrom(t => t.EmployeeType))
                .ForMember(dest => dest.Department, src => src.MapFrom(src => src.Department.Name));

            CreateMap<Employee, EmployeeDetailsDto>()
                .ForMember(dest => dest.EmployeeGender, g => g.MapFrom(g => g.EmployeeGender))
                .ForMember(dest => dest.EmployeeType, t => t.MapFrom(t => t.EmployeeType))
                .ForMember(dest => dest.Department, src => src.MapFrom(src => src.Department.Name));

        }
    }
}
