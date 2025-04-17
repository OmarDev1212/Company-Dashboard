using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
namespace Demo.DAL.Entities
{
    //    Employee entity should have the following properties Id[PK], Name, Age, Address, Is Active,
    //        Salary , Email , Phone Number, Hiring Date , Gender and EmployeeType
    [Flags]
    public enum EmployeeType
    {
        PartTime=1,
        FullTime=2
    }
    [Flags]
    public enum EmployeeGender
    {
        Male=1,
        Female=2,
    }
    public class Employee:BaseEntity
    {
        public string Name { get; set; }
        public int  Age { get; set; }
        public string Address { get; set; }
        public bool IsActive { get; set; }
        public decimal Salary { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateOnly HireDate { get; set; }
        public EmployeeType EmployeeType  { get; set; }
        public EmployeeGender EmployeeGender { get; set; }
    }
}
