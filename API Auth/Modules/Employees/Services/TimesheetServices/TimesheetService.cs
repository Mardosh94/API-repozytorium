using API_Auth.Data;
using API_Auth.Modules.Employees.Dtos;
using API_Auth.Modules.Employees.Entities;
using API_Auth.Modules.Shared;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace API_Auth.Modules.Employees.Services.TimesheetServices
{
    public class TimesheetService : ITimesheetService
    {
        private readonly AppDbContext _context;

        public TimesheetService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ServiceResult<Timesheet>> AddTimesheet(int employeeId, TimesheetDto timesheetDto)
        {
            try
            {
                var employee = await GetEmployeeFromDb(employeeId);
                if (employee == null)
                {
                    return ServiceResult<Timesheet>.Failure(ErrorMessages.NotFound);
                }

                var timesheet = timesheetDto.Adapt<Timesheet>();

                _context.Timesheets.Add(timesheet);
                await _context.SaveChangesAsync();

                return ServiceResult<Timesheet>.Success(timesheet);
            }
            catch
            {
                return ServiceResult<Timesheet>.Failure(ErrorMessages.InternalServerError);
            }
        }

        public async Task<ServiceResult> DeleteTimesheet(int employeeId, int timesheetId)
        {
            try
            {
                var employee = await GetEmployeeFromDb(employeeId);
                if (employee == null || !employee.Timesheets.Any(t => t.Id == timesheetId))
                {
                    return ServiceResult<Timesheet>.Failure(ErrorMessages.NotFound);
                }
                var timesheet = employee.Timesheets.First(t => t.Id == timesheetId);

                _context.Timesheets.Remove(timesheet);
                await _context.SaveChangesAsync();

                return ServiceResult.Success();
            }
            catch (Exception)
            {
                return ServiceResult<Timesheet>.Failure(ErrorMessages.InternalServerError);
            }
        }

        public async Task<ServiceResult<List<Timesheet>>> GetAllTimesheetByEmployeeId(int employeeId)
        {
            try
            {
                var employee = await GetEmployeeFromDb(employeeId);
                if (employee == null)
                {
                    return ServiceResult<List<Timesheet>>.Failure(ErrorMessages.NotFound);
                }
                if (employee.Timesheets != null && employee.Timesheets.Any())
                {
                    var timesheets = employee.Timesheets.ToList();
                    return ServiceResult<List<Timesheet>>.Success(timesheets);
                }
                else
                {
                    return ServiceResult<List<Timesheet>>.Failure(ErrorMessages.NotFound);
                }
            }
            catch (Exception)
            {
                return ServiceResult<List<Timesheet>>.Failure(ErrorMessages.InternalServerError);
            }
        }

        public async Task<ServiceResult<decimal>> GetTotalHours(int employeeId)
        {
            try
            {
                var employee = await GetEmployeeFromDb(employeeId);
                if (employee?.Timesheets == null || !employee.Timesheets.Any())
                {
                    return ServiceResult<decimal>.Failure(ErrorMessages.NotFound);
                }
                var totalHours = employee.Timesheets.Sum(t => t.TimeOfWorking);

                return ServiceResult<decimal>.Success(totalHours);
            }
            catch (Exception)
            {
                return ServiceResult<decimal>.Failure(ErrorMessages.InternalServerError);
            }
        }

        private async Task<Employee?> GetEmployeeFromDb(int id)
        {
            return await _context.Employees
            .Include(e => e.Timesheets)
            .FirstOrDefaultAsync(e => e.Id == id);
        }
    }
}
