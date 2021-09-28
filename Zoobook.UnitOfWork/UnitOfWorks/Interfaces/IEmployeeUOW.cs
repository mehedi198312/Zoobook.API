using Zoobook.Data;
using Zoobook.Repositories;

namespace Zoobook.UnitOfWork.Interfaces
{
    public interface IEmployeeUOW
    {
        Repository<Employee> EmployeeRepository { get; }

    }
}