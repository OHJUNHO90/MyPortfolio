using System.Diagnostics;
using WebApplication3.Models;

namespace WebApplication3.Services
{
    public class EmployeeService : IEmployeeService
    {
        readonly IDbService _dbService;
        public EmployeeService(IDbService dbService)
        {
            _dbService = dbService;
        }

        /// <summary>
        /// 
        /// </summary>
        public async Task<bool> CreateEmployee(Employee employee)
        {
            var result = await _dbService.EditData("insert into employee (name) values (@name)", employee);
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        public async Task<bool> DeleteEmployee(string name)
        {
            var deleteEmployee = await _dbService.EditData("delete from test1 where name=@name", new { name });
            return true;
        }

        public async Task<Employee?> GetEmployee(string name)
        {
            var employee = await _dbService.GetAsync<Employee>("select * from test1 where name=@name", new { name });
            return employee;
        }

        public async Task<List<Employee>> GetEmployeeList()
        {
            var employeeList = await _dbService.GetAll<Employee>("select * from test1");
            return employeeList;
        }

        /// <summary>
        /// 
        /// </summary>
        public async Task<Employee> UpdateEmployee(Employee employee)
        {
            var updateEmployee = await _dbService.EditData( "update employee SET name=@name " +
                                                            "where name=@name", employee);

            return employee;
        }
    }
}
