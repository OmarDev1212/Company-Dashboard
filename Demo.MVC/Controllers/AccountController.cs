﻿using Demo.DAL.Entities;
using Demo.MVC.Helpers;
using Demo.MVC.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NuGet.Common;
using System.Threading.Tasks;

namespace Demo.MVC.Controllers
{
    public class AccountController(UserManager<ApplicationUser> _userManager, SignInManager<ApplicationUser> _signInManager) : Controller
    {
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = new ApplicationUser
            {
                UserName = model.UserName,
                Email = model.Email,
                PasswordHash = model.Password,
                FirstName = model.FirstName,
                LastName = model.LastName,
                IsAgree = model.IsAgree,
            };
            var identityResult = await _userManager.CreateAsync(user, model.Password);
            if (identityResult.Succeeded)
            {
                TempData["SuccessMessage"] = "User Created successfully";
                return RedirectToAction(nameof(Login));
            }
            else
            {
                foreach (var error in identityResult.Errors)
                {
                    ModelState.TryAddModelError(error.Code, error.Description);
                }
                TempData["ErrorMessage"] = "Something went wrong";
                return View(model);
            }
        }


        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user is null) ModelState.AddModelError("", "Invalid Login attempt");
            var flag = await _userManager.CheckPasswordAsync(user, model.Password);
            if (flag)
            {
                var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);
                if (result.IsLockedOut)
                    ModelState.AddModelError("", "Account is lockedout");
                if (result.IsNotAllowed)
                    ModelState.AddModelError("", "You are not allowed to enter this site");

                if (result.Succeeded)
                    return RedirectToAction("Index", "Home");


            }
            return View(model);

        }

        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(Login));
        }

        public IActionResult ForgetPassword() => View();

        public IActionResult CheckYourInbox() => View();
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SendEmail(ForgetPasswordViewModel model)
        {
            if (!ModelState.IsValid) return View(model);
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user is null)
                ModelState.AddModelError("", "Invalid Email");
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var link = Url.Action("ResetPassword", "Account",
                new { email = user.Email, token = token }, Request.Scheme);
            var email = new Email()
            {
                To = model.Email,
                Subject = "Reset Your Password",
                Body = link
            };
            EmailSettings.SendEmail(email);
            return RedirectToAction(nameof(CheckYourInbox));
        }

        public IActionResult ResetPassword(string email, string token)
        {
            TempData["email"] = email;
            TempData["token"] = token;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid) return View(model);
            var email = TempData["email"] as string;
            var token = TempData["token"] as string;
            var user = await _userManager.FindByEmailAsync(email);
            if (user is null) ModelState.AddModelError("", "Invalid EMail");
            var result = await _userManager.ResetPasswordAsync(user, token, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction(nameof(Login));
            }
            return View(model);

        }


    }
}
