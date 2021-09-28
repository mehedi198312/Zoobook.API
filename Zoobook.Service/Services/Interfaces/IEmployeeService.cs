using System.Threading.Tasks;
using Zoobook.Model;

namespace Zoobook.Service.Interfaces
{
    public interface IEmployeeService
    {
        Task<BaseResponse> GetEmployeeList();
        Task<BaseResponse> GetEmployee(string id);
        Task<BaseResponse> InsertEmployee(EmployeeModel model);
        Task<BaseResponse> UpdateEmployee(EmployeeModel model);
        Task<BaseResponse> DeleteEmployee(string id);
    }
}