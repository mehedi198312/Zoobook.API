using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Zoobook.Model;
using Zoobook.Service.Interfaces;

namespace Zoobook.API.Controllers
{
    [EnableCors("AllowZooBookWebApp")]
    [Route("[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        //[Authorize]
        [HttpGet("get-employee")]
        public async Task<IActionResult> GetEmployee(string id)
        {
            try
            {
                return Ok(await _employeeService.GetEmployee(id));
            }
            catch (Exception e)
            {
                return NotFound();
            }
        }

        //[Authorize]
        [HttpGet("get-employee-list")]
        public async Task<IActionResult> GetEmployeeList()
        {
            try
            {
                return Ok(await _employeeService.GetEmployeeList());
            }
            catch (Exception e)
            {
                return NotFound();
            }
        }

        //[Authorize]
        [HttpPost("insert-employee")]
        public async Task<IActionResult> InsertEmployee(EmployeeModel model)
        {
            try
            {
                return Ok(await _employeeService.InsertEmployee(model));
            }
            catch (Exception e)
            {
                return NotFound();
            }
        }

        //[Authorize]
        [HttpPut("update-employee")]
        public async Task<IActionResult> UpdateEmployee(EmployeeModel model)
        {
            try
            {
                return Ok(await _employeeService.UpdateEmployee(model));
            }
            catch (Exception e)
            {
                return NotFound();
            }
        }

        //[Authorize]
        [HttpDelete("delete-employee")]
        public async Task<IActionResult> DeleteEmployee(string id)
        {
            try
            {
                return Ok(await _employeeService.DeleteEmployee(id));
            }
            catch (Exception e)
            {
                return NotFound();
            }
        }

    }
}