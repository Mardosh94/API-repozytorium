using API_Auth.Data;
using API_Auth.Modules.Employees.Dtos;
using API_Auth.Modules.Employees.Entities;
using API_Auth.Modules.Shared;
using Microsoft.AspNetCore.Mvc;
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

        public async Task<ServiceResult<TimesheetDto>> AddTimesheet(TimesheetDto timesheetDto)
        {
            try
            {
                var timesheetDto = timesheetDto.Adapt<Timesheet>(); // Mapowanie z Dto na db model

                _context.Employees.Add(timesheetDto);

                await _context.SaveChangesAsync();
                return ServiceResult<Employee>.Success(timesheetDto);
            }
            catch (Exception ex)
            {
                return ServiceResult<Employee>.Failure(ex.Message);
            }

        }

    }
}
