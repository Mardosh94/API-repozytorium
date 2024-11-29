using API_Auth.Entities;
using API_Auth.Features.Employees.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace API_Auth.Features.Employees.Services.TimesheetServices
{
    public interface ITimesheetService
    {
        Task<List<Timesheet>> GetTimesheetsById(int id);
    }
}
