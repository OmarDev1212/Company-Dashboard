using Demo.BLL.Services.EmployeeServices;
using Microsoft.AspNetCore.Mvc;

namespace Demo.MVC.Controllers
{
    public class EmployeeController(IEmployeeService _employeeService) : Controller
    {
        public IActionResult Index()
        {
            var emps=_employeeService.GetAllEmployees();
            return View(emps);
        }
        public IActionResult Details(int? id) {

            if (id == null) return BadRequest();
            var employee = _employeeService.GetById(id.Value);
            return View(employee);

        }
    }
}
