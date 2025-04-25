using Demo.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAL.Repositories.interfaces
{
    
    public interface IDepartmentRepository:IGenericRepository<Department>
    {                   
        //public IEnumerable<Department> GetAllDepartments(bool withTracking=false); // IEnumerable to iterate on rows to put them in view  also gets all data from database and load it in memory
        //                                                                    //IQueryable used to filter data in database not in application direct
        //                                                                    //ICollection used for need for Opertions like add,update,delete
        //                                                                    // IReadOnlyList used only for read and no need for iteration used in API as returned data is json
        //public Department GetDepartmentById(int id);
        //public int UpdateDepartment(Department department); //int to return no of affected rows in database
        //public int AddDepartment(Department department);
        //public int DeleteDepartment(Department department);

    }
}
