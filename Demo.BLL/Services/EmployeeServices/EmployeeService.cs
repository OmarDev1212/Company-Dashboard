using AutoMapper;
using Demo.BLL.DTO.Employee;
using Demo.BLL.Services.AttachmentService;
using Demo.DAL.Entities;
using Demo.DAL.Repositories.interfaces;

namespace Demo.BLL.Services.EmployeeServices
{
    public class EmployeeService(/*IEmployeeRepository _employeeRepository*/ IUnitOfWork _unitOfWork, IMapper _mapper, IAttachmentService _attachmentService) : IEmployeeService
    {
        public int CreateNewEmployee(CreatedEmployeeDto employee)
        {
            var mappedEmployee = _mapper.Map<CreatedEmployeeDto, Employee>(employee);
            if (employee.Image is not null)
            {
                mappedEmployee.ImageName = _attachmentService.UploadAttachment(employee.Image, "images");
            }
            _unitOfWork.EmployeeRepository.AddEntity(mappedEmployee);
            return _unitOfWork.SaveChanges();
        }

        public bool DeleteEmployee(int id)//soft delete
        {
            var employee = _unitOfWork.EmployeeRepository.GetEntityById(id);
            if (employee == null) return false;
            employee.IsDeleted = true;
            _attachmentService.DeleteAttachment(employee.ImageName, "images");
            //employee.ImageName= null;  //if you want to delete from db
            _unitOfWork.EmployeeRepository.UpdateEntity(employee);
            return _unitOfWork.SaveChanges() > 0;
        }

        public IEnumerable<EmployeeDto> GetAllEmployees()
        {
            var employees = _unitOfWork.EmployeeRepository.GetAll();
            return _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeDto>>(employees);

        }

        public EmployeeDetailsDto GetById(int id)
        {
            var employee = _unitOfWork.EmployeeRepository.GetEntityById(id);
            if (employee == null) return null;
            return _mapper.Map<Employee, EmployeeDetailsDto>(employee);
        }

        public IEnumerable<EmployeeDto> SearchEmployeesByName(string name)
        {
            var employees = _unitOfWork.EmployeeRepository.SearchByName(name);
            return _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeDto>>(employees);
        }

        public int UpdateEmployee(UpdateEmployeeDto employee)
        {
            var mappedEmp = _mapper.Map<UpdateEmployeeDto, Employee>(employee);
            if (employee.Image != null)
            {
                // Delete the OLD image (not the new uploaded file's name)
                if (!string.IsNullOrEmpty(employee.Image.FileName))
                {
                    _attachmentService.DeleteAttachment(employee.Image.FileName, "images");
                }

                // Upload the NEW image
                mappedEmp.ImageName = _attachmentService.UploadAttachment(employee.Image, "images");
            }
            else
            {
                // No new image uploaded, keep the existing image
                mappedEmp.ImageName = employee.ExistingImageName;
            }
            _unitOfWork.EmployeeRepository.UpdateEntity(mappedEmp);
            return _unitOfWork.SaveChanges();
        }
    }
}
