using API_Auth.Data;
using API_Auth.Modules.Employees.Dtos;
using API_Auth.Modules.Employees.Entities;
using API_Auth.Modules.Employees.Services.TimesheetServices;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_Auth.Modules.Employees.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TimesheetsController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly ITimesheetService _timesheetService;

        public TimesheetsController(AppDbContext context, ITimesheetService timesheetService)
        {
            _context = context;
            _timesheetService = timesheetService;

        }
        // CREATE: POST /Timesheets
        [HttpGet("sum/{id}")]
        //[Authorize]
        public async Task<ActionResult<TimesheetDto>> GetEmployeeWithTimesheet(int id)
        {
            var employee = await _context.Employees
           .Include(e => e.Timesheets)
           .FirstOrDefaultAsync(e => e.Id == id);

            if (employee == null) 
                return NotFound();

            decimal totalTimeWorked = employee.Timesheets.Sum(t => t.TimeOfWorking);

            var employeeTimesheet = new EmployeeTimesheetDto
            {
                Id = employee.Id,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                TotalWorkedTime = totalTimeWorked,
                Timesheets = employee.Timesheets.Select(t => new TimesheetDto
                {
                    Date = t.Date,
                    TimeOfWorking = t.TimeOfWorking
                }).ToList()
            };

            return Ok(employeeTimesheet);
        }

        // POST api/employees/{employeeId}/timesheet
        [HttpPost("{employeeId}/timesheet")]
        public async Task<IActionResult> AddTimesheet(int employeeId, [FromBody] TimesheetDto timesheetDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _timesheetService.AddTimesheet(employeeId, timesheetDto);

            if (result.IsSuccess)
                return Created($"Timesheet/{result.Data.Id.ToString()}", result.Data);
            else
                return StatusCode(500, result.ErrorMessege);




            // Dodajemy nowy Timesheet do bazy danych
            employee.Timesheets.Add();
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetEmployeeWithTimesheet), new { id = employeeId }, timesheet);
        }

    }
}
