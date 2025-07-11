﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAL.Entities
{
    public class Department:BaseEntity
    {

        public string Name { get; set; }
        public string? Description { get; set; }
        public string Code { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }=new HashSet<Employee>();
    }
}
