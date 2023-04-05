using WebApplication3.Models;

namespace WebApplication3.Services
{
    public interface IEmployeeService
    {
        Task<Employee?> GetEmployee(string name);
        Task<List<Employee>> GetEmployeeList();
        Task<bool> CreateEmployee(Employee employee);
        Task<bool> DeleteEmployee(string name);
        Task<Employee> UpdateEmployee(Employee employee);

    }
}
