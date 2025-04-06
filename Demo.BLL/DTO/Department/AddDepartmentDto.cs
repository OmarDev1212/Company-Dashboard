﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.DTO.Department
{
    public class AddDepartmentDto
    {
        public string Name { get; set; }    
        public string Code { get; set; }
        public string? Description { get; set; }
        [Display(Name = "Date Of Creation")]
        public DateOnly DateOfCreation { get; set; }
    }
}
