using AutoMapper;
using Zoobook.Data;
using Zoobook.Model;

namespace Zoobook.Service.Helpers
{
    public class AutoMapperProfile : Profile
    {
        // mappings between model and entity objects
        public AutoMapperProfile()
        {

            CreateMap<Employee, EmployeeModel>();

            CreateMap<EmployeeModel, Employee>();
        }
    }
}