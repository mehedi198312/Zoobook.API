using Zoobook.Data;

namespace Zoobook.Repositories
{
    public class EmployeeRepository : Repository<Employee>
    {
        public EmployeeRepository(EmployeeRecordsDbContext context) : base(context)
        {
        }
    }
}