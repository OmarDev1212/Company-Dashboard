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
    public class DepartmentRepository(ApplicationDbContext _context) : IDepartmentRepository
    {
        public int AddDepartment(Department department)
        {
            _context.Add(department);
            return _context.SaveChanges();
        }

        public int DeleteDepartment(Department department)
        {
                _context.Remove(department);
            return _context.SaveChanges();
        }

        public IEnumerable<Department> GetAllDepartments(bool withTracking=false)
        {
            if(withTracking)
                return _context.Departments.ToList();
            else
                return _context.Departments.AsNoTracking().ToList();
        }

        public Department GetDepartmentById(int id)
        {
            return _context.Departments.Find(id);
        }

        public int UpdateDepartment(Department department)
        {
            _context.Update(department);
            return _context.SaveChanges();
        }
    }
}
