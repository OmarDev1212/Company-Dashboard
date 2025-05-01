using Demo.BLL.DTO.Employee;
using Demo.DAL.Entities;
using Demo.MVC.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Demo.MVC.Controllers
{
    public class UserController(UserManager<ApplicationUser> _userManager, IWebHostEnvironment _environment, ILogger<UserController> _logger) : Controller
    {
        public async Task<IActionResult> Index(string? searchValue)
        {
            IEnumerable<UserViewModel> users;
            if (string.IsNullOrEmpty(searchValue))
            {

                users = await _userManager.Users.Select(u =>
                   new UserViewModel()
                   {
                       Id = u.Id,
                       Email = u.Email,
                       FirstName = u.FirstName,
                       LastName = u.LastName,
                       PhoneNumber = u.PhoneNumber,
                       Roles = _userManager.GetRolesAsync(u).Result
                   }).ToListAsync();

                return View(users);
            }
            else
            {
                users = await _userManager.Users.Where(u => u.Email.Contains(searchValue)).Select(u =>
                  new UserViewModel()
                  {
                      Id = u.Id,
                      Email = u.Email,
                      FirstName = u.FirstName,
                      LastName = u.LastName,
                      PhoneNumber = u.PhoneNumber,
                      Roles = _userManager.GetRolesAsync(u).Result
                  }).ToListAsync();

                return View(users);
            }
        }

        public async Task<IActionResult> Details(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return BadRequest();
            var userToReturn = new UserViewModel()
            {
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber,
                Roles = _userManager.GetRolesAsync(user).Result
            };
            return View(userToReturn);
        }
        public async Task<IActionResult> Edit(string? id)
        {

            if (id is null) return BadRequest();
            var user = await _userManager.FindByIdAsync(id);
            if (user is null) return NotFound();
            var userToReturn = new UserViewModel()
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Roles = await _userManager.GetRolesAsync(user)
            };
            return View(userToReturn);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] string id, UserViewModel model)
        {
            if (id != model.Id) return BadRequest();
            if (!ModelState.IsValid) return View(model);
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user is null) return NotFound();
            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                if (_environment.IsDevelopment())
                {
                    TempData["ErrorMessage"] = "Something went wrong";
                    ModelState.AddModelError(string.Empty, "Data did not ");
                }
                else
                {
                    TempData["ErrorMessage"] = "Something went wrong";
                    _logger.LogError("Editing Data is not completed successfully");
                }
                return View(model);
            }
        }
        public async Task<IActionResult> Delete(string? id)
        {
            if (id is null) return BadRequest();
            var user = await _userManager.FindByIdAsync(id);
            if (user is null) return NotFound();
            var userToReturn = new UserViewModel()
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Roles = await _userManager.GetRolesAsync(user)
            };
            return View(userToReturn);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmDelete(string id)
        {
            if (id == null) return BadRequest();
            var user = await _userManager.FindByIdAsync(id);
            if (user is null) return NotFound();
            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                if (_environment.IsDevelopment())
                {
                    TempData["ErrorMessage"] = "Something went wrong";
                    ModelState.AddModelError(string.Empty, " Data is not deleted successfully");
                }
                else
                {
                    TempData["ErrorMessage"] = "Something went wrong";
                    _logger.LogError("Data is not deleted successfully");
                }
                ModelState.AddModelError("", "Invalid Operation");
                return RedirectToAction(nameof(Delete), new { id });
            }
        }
    }
}
