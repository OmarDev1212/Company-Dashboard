﻿using Demo.DAL.Entities;
using System.ComponentModel.DataAnnotations;

namespace Demo.MVC.ViewModels
{
    public class EmployeeViewModel 
    {
        [MaxLength(50, ErrorMessage = "Max length should be 50 character")]
        [MinLength(5, ErrorMessage = "Min length should be 5 characters")]
        public string Name { get; set; }
        [Range(24, 50)]
        public int Age { get; set; }
        [RegularExpression("[1-9]{1,3}-[a-zA-z]{5,10}-[a-zA-z]{4,10}-[a-zA-z]{4,10}")]
        public string Address { get; set; } //Address Should be In Format [123-Street-City-Country]

        public bool IsActive { get; set; }
        public decimal Salary { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [Phone]
        [Display(Name = "Phone Number")]

        public string PhoneNumber { get; set; }
        [Display(Name = "Employee Gender")]

        public string EmployeeGender { get; set; }
        [Display(Name = "Employee Type")]
        public string EmployeeType { get; set; }
        [Display(Name = "Hiring Date")]
        public DateOnly HireDate { get; set; }
        public Department? Department { get; set; }
        public int? DepartmentId { get; set; }
        public IFormFile? Image { get; set; }
        public string? ImageName { get; set; }
        public string? ExistingImageName { get; set; }

    }
}
