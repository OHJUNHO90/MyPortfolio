using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebApplication3.Models;
using WebApplication3.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplication3.Controllers
{
    [Produces("application/json")]
    [Route("[Controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        readonly IEmployeeService _employeeService;
        public EmployeeController(IEmployeeService _employee) 
        {
            this._employeeService = _employee;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _employeeService.GetEmployeeList();
            return Ok(result);
        }

        [HttpDelete("{name}")]
        public async Task<IActionResult> DeleteEmployee(string name)
        {
            var result = await _employeeService.DeleteEmployee(name);
            return Ok(result);
        }


        [HttpGet("{name}")]
        public async Task<IActionResult> GetEmployee(string name)
        {
            var result = await _employeeService.GetEmployee(name);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddEmployee([FromBody] Employee employee)
        {
            var result = await _employeeService.CreateEmployee(employee);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateEmployee([FromBody] Employee employee)
        {
            var result = await _employeeService.UpdateEmployee(employee);
            return Ok(result);
        }
    }
}
