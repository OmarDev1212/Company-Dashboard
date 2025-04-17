using Demo.BLL.Repositories.interfaces;
using Demo.DAL.Data;
using Demo.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Repositories.repos
{
    public class DepartmentRepository(ApplicationDbContext context) : GenericRepository<Department>(context), IDepartmentRepository
    {
        public int AddDepartment(Department department)
        {
            context.Add(department);
            return context.SaveChanges();
        }

        public int DeleteDepartment(Department department)
        {
                context.Remove(department);
            return context.SaveChanges();
        }

        public IEnumerable<Department> GetAllDepartments(bool withTracking=false)
        {
            if(withTracking)
                return context.Departments.ToList();
            else
                return context.Departments.AsNoTracking().ToList();
        }

        public Department GetDepartmentById(int id)
        {
            return context.Departments.Find(id);
        }   

        public int UpdateDepartment(Department department)
        {
            context.Update(department);
            return context.SaveChanges();
        }
    }
}
