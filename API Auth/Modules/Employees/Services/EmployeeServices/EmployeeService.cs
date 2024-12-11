using API_Auth.Data;
using API_Auth.Modules.Employees.Dtos;
using API_Auth.Modules.Employees.Entities;
using API_Auth.Modules.Shared;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace API_Auth.Modules.Employees.Services.EmployeeServices
{


    //public class

    public class EmployeeService : IEmployeeService
    {
        private readonly AppDbContext _context;

        public EmployeeService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ServiceResult<Employee>> AddEmployee(EmployeeDto employeeDto)
        {
            try
            {
               var employeeDb = employeeDto.Adapt<Employee>(); // Mapowanie z Dto na db model

                _context.Employees.Add(employeeDb);

                await _context.SaveChangesAsync();
                return ServiceResult<Employee>.Success(employeeDb);
            }
            catch (Exception ex)
            {
                return ServiceResult<Employee>.Failure(ex.Message);
            }
            

        }

        public async Task<ServiceResult> DeleteEmployee(int id)
        {
            var employeeResult = await GetEmployeeFromDb(id);
            if (employeeResult == null)
                return ServiceResult.Failure(ErrorMessages.NotFound);
            try
            {
                // Usuwanie powiązanych danych
                if (employeeResult.Address != null)
            {
                _context.Addresses.Remove(employeeResult.Address);
            }

            if (employeeResult.Timesheets != null)
            {
                _context.Timesheets.RemoveRange(employeeResult.Timesheets);
            }

            // Usunięcie samego pracownika
            _context.Employees.Remove(employeeResult);
            await _context.SaveChangesAsync();

            }
            catch (Exception)
            {
                return ServiceResult.Failure(ErrorMessages.InternalServerError);
            }

            return ServiceResult.Success();
        }
        public async Task<ServiceResult<Employee>> GetEmployeeById(int id)
        {
            var employee = await GetEmployeeFromDb(id);
            if (employee == null)
                return ServiceResult<Employee>.Failure("Employee not exists.");

            return ServiceResult<Employee>.Success(employee);
        }

        private async Task<Employee?> GetEmployeeFromDb(int id)
        {
            return await _context.Employees
            .Include(e => e.Address)
            .Include(e => e.Timesheets)
            .FirstOrDefaultAsync(e => e.Id == id);
        }

        
        public async Task<List<Employee>> GetAllEmployees()//czy to może iść do shared?
        {
            var employees = await _context.Employees
            .Include(e => e.Address)
            .Include(e => e.Timesheets)
            .ToListAsync();
            return employees;
        }

        public async Task<ServiceResult<Employee>> UpdateEmployee(int id, EmployeeDto employeeDto)
        {
            var employeeFromDb = await GetEmployeeFromDb(id);
            if (employeeFromDb == null)
                return ServiceResult<Employee>.Failure(ErrorMessages.NotFound);

            employeeFromDb.FirstName = employeeDto.FirstName;
            employeeFromDb.LastName = employeeDto.LastName;
            employeeFromDb.Email = employeeDto.Email;
            employeeFromDb.PhoneNumber = employeeDto.PhoneNumber;
            employeeFromDb.DateOfBirth = employeeDto.DateOfBirth;
            if (employeeFromDb.Address != null)
            {
                employeeFromDb.Address.City = employeeDto.Address.City;
                employeeFromDb.Address.PostCode = employeeDto.Address.PostCode;
                employeeFromDb.Address.Street = employeeDto.Address.Street;
                employeeFromDb.Address.BuildingNumber = employeeDto.Address.BuildingNumber;
            }

            try
            {
            _context.Employees.Update(employeeFromDb);
            await _context.SaveChangesAsync();
                return ServiceResult<Employee>.Success(employeeFromDb);
            }
            catch (Exception)
            {
               return ServiceResult<Employee>.Failure(ErrorMessages.InternalServerError);
            }
        }
    }
}
