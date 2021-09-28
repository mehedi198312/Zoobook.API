using Zoobook.Data;
using Zoobook.Repositories;
using Zoobook.UnitOfWork.Interfaces;

namespace Zoobook.UnitOfWork
{
    public class EmployeeUOW : IEmployeeUOW
    {
        private Repository<Employee> _employeeRepository;

        #region "AppDbContext variable"
        private readonly EmployeeRecordsDbContext _context;
        public EmployeeUOW(EmployeeRecordsDbContext appDbContext)
        {
            _context = appDbContext;
        }
        #endregion

        public Repository<Employee> EmployeeRepository => _employeeRepository ?? (_employeeRepository = new EmployeeRepository(_context));
        
        public int Commit()
        {
            return _context.SaveChanges();
        }

    }
}