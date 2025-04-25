using Demo.DAL.Data;
using Demo.DAL.Entities;
using Demo.DAL.Repositories.interfaces;
using Microsoft.EntityFrameworkCore;

namespace Demo.DAL.Repositories.repos
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

        public void UpdateDepartment(Department department)
        {
            context.Update(department);
        }
    }
}
