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
        public IActionResult Index(string? searchValue)
        {
            //Binding => Sending data from action to view's Dictionary [one way direction] view can't send response to action

            //ViewData 
            //Explicit Casting is needed as value of dictionary is object
            string data = ViewData["Message"] as string;

            //ViewBag => Dynamic property
            //CLR know data type in RunTime 
            //No Need For Explicit casting
            ViewBag.Message = "Hello from viewBag";


            TempData.Keep(); //keeps the data with index view if next action needed it.
            IEnumerable<EmployeeDto> emps;
            if (string.IsNullOrEmpty(searchValue))
            {
                emps = _employeeService.GetAllEmployees();
                return View(emps);
            }
            else
            {
                emps = _employeeService.SearchEmployeesByName(searchValue);
                return View(emps);
            }
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
        public IActionResult Create(EmployeeViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var mappedEmployee = new CreatedEmployeeDto()
                    {
                        Address = model.Address,
                        Age = model.Age,
                        Email = model.Email,
                        EmployeeGender = model.EmployeeGender,
                        EmployeeType = model.EmployeeType,
                        HireDate = model.HireDate,
                        IsActive = model.IsActive,
                        Name = model.Name,
                        PhoneNumber = model.PhoneNumber,
                        Salary = model.Salary,
                        DepartmentId = model.DepartmentId,
                        Image = model.Image
                    };

                    var result = _employeeService.CreateNewEmployee(mappedEmployee);
                    if (result > 0)
                    {
                        //temp data send data from action to action by dictionary which is different from the dictionary of viewbag and viewdata 

                        TempData["SuccessMessage"] = "Employee created successfully";
                        return RedirectToAction(nameof(Index));

                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Employee is not created successfully";
                        ModelState.AddModelError(string.Empty, "Employee is not  created successfully");
                        return View(model);
                    }
                }
                catch (Exception ex)
                {
                    if (_environment.IsDevelopment())
                    {
                        TempData["ErrorMessage"] = "Something went wrong";
                        ModelState.AddModelError(string.Empty, ex.Message);
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Something went wrong";
                        _logger.LogError(ex.Message);
                    }
                }
            }
            return View(model);
        }

        public IActionResult Edit(int? id)
        {
            if (id is null) return BadRequest();
            var emp = _employeeService.GetById(id.Value);
            if (emp is null) return NotFound();
            var empToReturn = new EmployeeViewModel()
            {
                Address = emp.Address,
                Age = emp.Age,
                Email = emp.Email,
                EmployeeGender = emp.EmployeeGender,
                EmployeeType = emp.EmployeeType,
                HireDate = emp.HireDate,
                IsActive = emp.IsActive,
                Name = emp.Name,
                PhoneNumber = emp.PhoneNumber,
                Salary = emp.Salary,
                ImageName = emp.ImageName,
                Image=emp.Image,
                ExistingImageName = emp.ImageName
            };

            return View(empToReturn);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int id, EmployeeViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);
            try
            {
                var employeeDto = new UpdateEmployeeDto()
                {
                    Id = id,
                    Address = model.Address,
                    Age = model.Age,
                    Email = model.Email,
                    Image = model.Image,
                    DepartmentId = model.DepartmentId,
                    EmployeeGender = model.EmployeeGender,
                    EmployeeType = model.EmployeeType,
                    HireDate = model.HireDate,
                    IsActive = model.IsActive,
                    Name=model.Name,
                    PhoneNumber=model.PhoneNumber,
                    Salary=model.Salary,
                    ExistingImageName = model.ExistingImageName 
                };
                var result = _employeeService.UpdateEmployee(employeeDto);
                if (result > 0)
                {
                    TempData["SuccessMessage"] = "Employee's data was updated successfully";

                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                if (_environment.IsDevelopment())
                {
                    TempData["ErrorMessage"] = "Something went wrong";
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
                else
                {
                    TempData["ErrorMessage"] = "Something went wrong";
                    _logger.LogError(ex.Message);
                }
            }
            return View(model);
        }


        public IActionResult Delete(int? id)
        {

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
                {
                    TempData["SuccessMessage"] = "Employee's data deleted successfully";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    TempData["ErrorMessage"] = "Something went wrong";
                    ModelState.AddModelError(string.Empty, "Employee is not deleted");
                    return RedirectToAction(nameof(Delete), new { id });
                }
            }
            catch (Exception ex)
            {
                if (_environment.IsDevelopment())
                {
                    TempData["ErrorMessage"] = "Something went wrong";
                    ModelState.AddModelError(string.Empty, ex.Message);
                    return RedirectToAction(nameof(Delete), new { id });
                }
                else
                {
                    TempData["ErrorMessage"] = "Something went wrong";
                    _logger.LogError(ex.Message);
                    return RedirectToAction("Error", "Home");
                }
            }
        }


    }
}
