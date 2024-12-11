using API_Auth.Modules.Employees.Dtos;
using API_Auth.Modules.Employees.Entities;
using API_Auth.Modules.Shared;

namespace API_Auth.Modules.Employees.Services.EmployeeServices
{
    public interface IEmployeeService
    {
        Task<ServiceResult<Employee>> AddEmployee(EmployeeDto employee);
        Task<ServiceResult> DeleteEmployee(int id);
        Task<ServiceResult<Employee>> GetEmployeeById(int id);
        Task<ServiceResult<Employee>> UpdateEmployee(int id, EmployeeDto employeeDto);
        Task<List<Employee>> GetAllEmployees();
    }
}
