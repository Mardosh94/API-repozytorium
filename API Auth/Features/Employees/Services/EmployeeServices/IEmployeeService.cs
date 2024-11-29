using API_Auth.Entities;
using API_Auth.Features.Employees.Dtos;

namespace API_Auth.Features.Employees.Services.EmployeeServices
{
    public interface IEmployeeService
    {
        Task<Employee> AddEmployee(EmployeeDto employee);
        Task<bool> DeleteEmployee(int id);
        Task<Employee> GetEmployeeById(int id);
        Task<List<Employee>> GetAllEmployees();
    }
}
