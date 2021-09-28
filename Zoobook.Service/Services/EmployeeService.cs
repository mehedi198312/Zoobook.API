using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Zoobook.Data;
using Zoobook.Model;
using Zoobook.Service.Interfaces;
using Zoobook.UnitOfWork.Interfaces;

namespace Zoobook.Service
{
    public class EmployeeService : IEmployeeService
    {
        #region properties

        private readonly IEmployeeUOW _employeeUOW;
        private readonly IMapper _mapper;

        #endregion

        #region constructor

        public EmployeeService(IEmployeeUOW employeeUOW, IMapper mapper)
        {
            _employeeUOW = employeeUOW;
            _mapper = mapper;
        }

        #endregion

        public async Task<BaseResponse> GetEmployeeList()
        {
            var baseResponse = new BaseResponse();
            var value = await _employeeUOW.EmployeeRepository.GetList();
            if (value != null)
            {
                var result = _mapper.Map<List<EmployeeModel>>(value);
                baseResponse.IsSuccessful = true;
                baseResponse.Data = result;
                baseResponse.Message = "Information Found.";
            }
            else
            {
                baseResponse.IsSuccessful = false;
                baseResponse.Message = "Information not Found.";
            }

            return baseResponse;
        }

        public async Task<BaseResponse> GetEmployee(string id)
        {
            var baseResponse = new BaseResponse();
            var value = await _employeeUOW.EmployeeRepository.FirstOrDefaultAsync(f=> f.Id==id);
            if (value != null)
            {
                var result = _mapper.Map<EmployeeModel>(value);
                baseResponse.IsSuccessful = true;
                baseResponse.Data = result;
                baseResponse.Message = "Information Found.";
            }
            else
            {
                baseResponse.IsSuccessful = false;
                baseResponse.Message = "Information not Found.";
            }
            return baseResponse;
        }

        public async Task<BaseResponse> InsertEmployee(EmployeeModel model)
        {
            var baseResponse = new BaseResponse();
            var employee = await _employeeUOW.EmployeeRepository.Get(model.Id);
            if (employee == null)
            {
                var valEmployee = _mapper.Map<Employee>(model);
                var value = await _employeeUOW.EmployeeRepository.Insert(valEmployee);
                baseResponse.IsSuccessful = true;
                baseResponse.Data = value.Id;
                baseResponse.Message = "Successfully Saved.";
            }
            else
            {
                baseResponse.IsSuccessful = false;
                baseResponse.Message = "Employee id already exist.";
            }

            
            return baseResponse;
        }

        public async Task<BaseResponse> UpdateEmployee(EmployeeModel model)
        {
            var baseResponse = new BaseResponse();

            var employee = await _employeeUOW.EmployeeRepository.Get(model.Id);
            employee.FirstName = model.FirstName;
            employee.LastName = model.LastName;
            employee.MiddleName = model.MiddleName;
            
            await _employeeUOW.EmployeeRepository.Update(employee);

            baseResponse.IsSuccessful = true;
            baseResponse.Message = "Successfully Updated.";
            return baseResponse;
        }

        public async Task<BaseResponse> DeleteEmployee(string id)
        {
            var baseResponse = new BaseResponse();

             await _employeeUOW.EmployeeRepository.PermanentDeleteById(id);
            
            baseResponse.IsSuccessful = true;
            baseResponse.Message = "Successfully Deleted.";
            return baseResponse;
        }


    }
}