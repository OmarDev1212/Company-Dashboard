using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.DTO.Employee
{
    //Return Id[PK], Name, Age, Is Active, Salary , Email , Gender and EmployeeType
    public class EmployeeDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [Range(24, 50)]
        public int Age { get; set; }
        public bool IsActive { get; set; }
        [DataType(DataType.Currency)]
        public decimal Salary { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string EmployeeGender { get; set; }
        public string EmployeeType { get; set; }
        [Display(Name = "Hiring Date")]
        public DateOnly HireDate { get; set; }
        public string? Department { get; set; }
    }
}
