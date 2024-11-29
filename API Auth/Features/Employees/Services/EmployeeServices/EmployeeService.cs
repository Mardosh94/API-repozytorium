using API_Auth.Entities;
using API_Auth.Features.Employees.Dtos;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace API_Auth.Features.Employees.Services.EmployeeServices
{
    public class EmployeeService : IEmployeeService
    {
        private readonly AppDbContext _context;

        public EmployeeService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Employee> AddEmployee(EmployeeDto employee)
        {
            var employeeDb = employee.Adapt<Employee>(); // Mapowanie z Dto na db model

            _context.Employees.Add(employeeDb);

            await _context.SaveChangesAsync();
            
            return employeeDb;
        }

        public async Task<bool> DeleteEmployee(int id)
        {
            var employee = await GetEmployeeById(id);
            if (employee == null)
                return false;

            // Usuwanie powiązanych danych
            if (employee.Address != null)
            {
                _context.Addresses.Remove(employee.Address);
            }

            if (employee.Timesheets != null && employee.Timesheets.Any())
            {
                _context.Timesheets.RemoveRange(employee.Timesheets);
            }

            // Usunięcie samego pracownika
            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<Employee> GetEmployeeById(int id)
        {
            var employee = await _context.Employees
            .Include(e => e.Address)
            .Include(e => e.Timesheets)
            .FirstOrDefaultAsync(e => e.Id == id);
            return employee;
        }
        public async Task<List<Employee>> GetAllEmployees()
        {
            var employees = await _context.Employees
            .Include(e => e.Address)
            .Include(e => e.Timesheets)
            .ToListAsync();
            return employees;
        }
    }
}
