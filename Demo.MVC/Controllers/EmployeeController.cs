using Demo.BLL.DTO.Employee;
using Demo.BLL.Services.EmployeeServices;
using Demo.MVC.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Demo.MVC.Controllers
{
    public class EmployeeController(IEmployeeService _employeeService,
                                    ILogger<EmployeeController> _logger,
                                    IWebHostEnvironment _environment) : Controller
    {
        public IActionResult Index()
        {
            var emps = _employeeService.GetAllEmployees();
            return View(emps);
        }
        public IActionResult Details(int? id)
        {

            if (id == null) return BadRequest();
            var employee = _employeeService.GetById(id.Value);
            return View(employee);

        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreatedEmployeeDto model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = _employeeService.CreateNewEmployee(model);
                    if (result > 0)
                        return RedirectToAction(nameof(Index));
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Employee is not  created successfully");
                        return View(model);
                    }
                }
                catch (Exception ex)
                {
                    if (_environment.IsDevelopment())
                        ModelState.AddModelError(string.Empty, ex.Message);
                    else
                        _logger.LogError(ex.Message);
                }
            }
            return View(model);
        }

        public IActionResult Edit(int? id)
        {
            if (id is null) return BadRequest();
            var emp = _employeeService.GetById(id.Value);
            if(emp is null) return NotFound();
            var empToReturn = new EditEmployeeViewModel()
            {
                Address = emp.Address,
                Age = emp.Age,
                Email = emp.Email,
                EmployeeGender = emp.EmployeeGender,
                EmployeeType = emp.EmployeeType,
                HireDate = emp.HireDate,
                IsActive = emp.IsActive,
                Name = emp.Name,
                PhoneNumber=emp.PhoneNumber,
                Salary=emp.Salary,
            };

            return View(empToReturn);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int id, UpdateEmployeeDto model)
        {
          if(!ModelState.IsValid)
                return View(model);
            else
            {
                try
                {
                   var result= _employeeService.UpdateEmployee(model);
                    if (result > 0)
                        return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    if (_environment.IsDevelopment())
                        ModelState.AddModelError(string.Empty, ex.Message);
                    else
                        _logger.LogError(ex.Message);
                }
                return View(model);
            }
        }

        public IActionResult Delete(int? id) {

            if (id is null) return BadRequest();
            var employee = _employeeService.GetById(id.Value);
            if (employee is null) return NotFound();
            return View(employee);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ConfirmDelete(int? id)
        {
            if (id == 0) return BadRequest();
            try
            {
                var isdeleted = _employeeService.DeleteEmployee(id.Value);
                if (isdeleted)
                    return RedirectToAction(nameof(Index));
                else
                {
                    ModelState.AddModelError(string.Empty, "Employee is not deleted");
                    return RedirectToAction(nameof(Delete), new { id });
                }
            }
            catch (Exception ex)
            {
                if (_environment.IsDevelopment())
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                    return RedirectToAction(nameof(Delete), new { id });
                }
                else
                {
                    _logger.LogError(ex.Message);
                    return RedirectToAction("Error", "Home");
                }
            }
        }

    }
}
