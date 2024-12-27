
using API_Auth.Modules.Employees.Dtos;
using API_Auth.Modules.Employees.Entities;
using API_Auth.Modules.Shared;

namespace API_Auth.Modules.Employees.Services.TimesheetServices
{
    public interface ITimesheetService
    {
        Task<ServiceResult<Timesheet>> AddTimesheet(int employeeId, TimesheetDto timesheetDto);
        Task<ServiceResult> DeleteTimesheet(int employeeId, int timesheetId);
        Task<ServiceResult<decimal>> GetTotalHours(int employeeId);
        Task<ServiceResult<List<Timesheet>>> GetAllTimesheetByEmployeeId(int employeeId);
    }
}
