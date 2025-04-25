using Demo.BLL.DTO.Department;
using Demo.BLL.Services.DepartmentServices;
using Demo.MVC.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Demo.MVC.Controllers
{
    public class DepartmentController(IDepartmentService _departmentService
                                    ,ILogger<DepartmentController>_logger,
                                     IWebHostEnvironment _environment) : Controller
    {
        public IActionResult Index()
        {
            var departments = _departmentService.GetAllDepartments();
            return View(departments);
        }
        public IActionResult Details(int? id)
        {
            if (!id.HasValue) return BadRequest();
            var department = _departmentService.GetDepartmentById(id.Value);
            if (department is null) return NotFound();
            else return View(department);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(AddDepartmentDto department)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = _departmentService.AddDepartment(department);
                    if (result > 0)
                    {
                        TempData["SuccessMessage"] = "Department added successfully";
                        return RedirectToAction(nameof(Index));
                    }
                    else
                        return View(department);
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
            return View(department);
        }
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (!id.HasValue) return BadRequest();
            var department = _departmentService.GetDepartmentById(id.Value);
            if (department is null) return NotFound();
            var departmentToReturn = new DepartmentEditViewModel()
            {
                Name = department.Name,
                Code = department.Code,
                DateOfCreation = department.DateOfCreation,
                Description = department.Description,
            };
            return View(departmentToReturn);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int id, UpdateDepartmentDto department)
        {
            if (ModelState.IsValid)
            {

                try
                {
                    var result = _departmentService.UpdateDepartment(department);
                    if (result > 0)
                    {
                        TempData["SuccessMessage"] = "Department updated successfully";
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Something went wrong";
                        return View(department);
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
            return View(department);

        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var department = _departmentService.GetDepartmentById(id);
            return View(department);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ConfirmDelete(int id)
        {
            if (id == 0) return BadRequest();
            try
            {
               var isdeleted= _departmentService.DeleteDepartment(id);
                if (isdeleted)
                {
                    TempData["SuccessMessage"] = "Department was deleted successfully";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    TempData["ErrorMessage"] = "Something went wrong";
                    ModelState.AddModelError(string.Empty,"Department is not deleted");
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
                    return RedirectToAction("Error","Home");
                }
            }   
        }
    }
}
