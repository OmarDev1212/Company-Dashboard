using Demo.BLL.DTO.Department;
using Demo.BLL.Services.DepartmentServices;
using Demo.MVC.Controllers;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
namespace Demo.Test.Controllers
{

    public class DepartmentControllerTests
    {
        private readonly IDepartmentService _departmentService;
        private readonly ILogger<DepartmentController> _logger;
        private readonly IWebHostEnvironment _environment;
        private readonly DepartmentController _departmentController;

        public DepartmentControllerTests()
        {
            //dependencies
            _departmentService = A.Fake<IDepartmentService>();
            _logger = A.Fake<ILogger<DepartmentController>>();
            _environment = A.Fake<IWebHostEnvironment>();
            //sut
            _departmentController = new DepartmentController(_departmentService, _logger, _environment);
        }
        [Fact]
        public void DepartmentController_Index_ReturnsSuccess()
        {
            //arrange
            var departments = A.Fake<IEnumerable<DepartmentDto>>();
            A.CallTo(() => _departmentService.GetAllDepartments()).Returns(departments);
            //act
            IActionResult result = _departmentController.Index();
            //assert -> check your actions
            result.Should().BeOfType<ViewResult>();
        }
        [Theory]
        [InlineData(1)]
        public void DepartmentController_Details_ReturnsSuccess(int id)
        {
            //arrange
            var department = A.Fake<DepartmentDetailsDto>();
            A.CallTo(() => _departmentService.GetDepartmentById(id)).Returns(department);
            //act
            IActionResult result = _departmentController.Details(id);
            //assert -> check your actions
            result.Should().BeOfType<ViewResult>();
        }
        [Fact]
        public void DepartmentController_Create_ReturnsSuccess()
        {
            //act
            IActionResult result = _departmentController.Create();
            //assert -> check your actions
            result.Should().BeOfType<ViewResult>();
        }
        [Fact]
        public void DepartmentController_CreatePost_ReturnsSuccess()
        {
            //arrange
            var departmentDto = new AddDepartmentDto()
            {
                Name = "HR",
                Description = "Human Resources",
                Code="500",
                CreatedBy=1,
                DateOfCreation=DateOnly.MinValue,
                LastModifiedBy=1
            };
            A.CallTo(() => _departmentService.AddDepartment(departmentDto)).Returns(1);
            //act
            IActionResult result = _departmentController.Create(departmentDto);
            //assert -> check your actions
            result.Should().BeOfType<RedirectToActionResult>();
        }


    }
}
