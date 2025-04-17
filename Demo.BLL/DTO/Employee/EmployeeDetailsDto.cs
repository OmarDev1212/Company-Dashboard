using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.DTO.Employee
{
    //Return Id[PK], Name, Age, Address, Is Active, Salary , Email , Phone Number, HiringDate Gender and EmployeeType
    public class EmployeeDetailsDto
    {
        public int Id { get; set; }
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
        [Display(Name= "Employee Type")]
        public string EmployeeType { get; set; }
        [Display(Name="Hiring Date")]
        public DateOnly HiringDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int LastModifiedBy { get; set; }
        public DateTime LastModifiedOn { get; set; }
    }
}
