using Demo.DAL.Data;
using Demo.DAL.Repositories.interfaces;

namespace Demo.DAL.Repositories.repos
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly Lazy<IEmployeeRepository> _employeeRepository;
        private readonly Lazy<IDepartmentRepository> _departmentRepository;


        public UnitOfWork(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _employeeRepository = new Lazy<IEmployeeRepository>(()=>new EmployeeRepository(dbContext));
            _departmentRepository = new Lazy<IDepartmentRepository>(()=>new DepartmentRepository(dbContext));
        }
        public IDepartmentRepository DepartmentRepository=>_departmentRepository.Value;
        public IEmployeeRepository EmployeeRepository => _employeeRepository.Value;

        public int SaveChanges()=>_dbContext.SaveChanges();
    }
}
