using API_Auth.Entities;
using API_Auth.Features.Employees.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_Auth.Features.Employees.Services.TimesheetServices
{
    public class TimesheetService : ITimesheetService
    {
        private readonly AppDbContext _context;

        public TimesheetService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Timesheet>> GetTimesheetsById(int id)
        {
            var employee = await _context.Employees
            .Include(e => e.Timesheets)
            .FirstOrDefaultAsync(e => e.Id == id);
            return employee.Timesheets;
        }

    }
}
