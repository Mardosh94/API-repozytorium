using API_Auth.Entities;
using API_Auth.Features.Employees.Dtos;
using API_Auth.Features.Employees.Services.EmployeeServices;
using API_Auth.Features.Employees.Services.TimesheetServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_Auth.Controllers
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
        [HttpGet("{id}")]
        //[Authorize]
        public async Task<IActionResult> GetTimesheetsById(int id) 
        {
            var timesheets = await _timesheetService.GetTimesheetsById(id);
            return Ok(timesheets);
        }

        // CREATE: POST /Timesheets
        [HttpPost("{id}")]
        //[Authorize]
        public async Task<ActionResult<Employee>> AddTimesheet([FromBody] TimesheetDto timesheet, int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var employee = await _context.Employees
                .Include(e => e.Timesheets)
                .FirstOrDefaultAsync (e => e.Id == id);

        if (employee == null) 
            {
                return NotFound(new { message = "Employee not found." });
            }

            var newTimesheet = new Timesheet
            {
                Date = timesheet.Date,
                TimeOfWorking = timesheet.TimeOfWorking
            };

            employee.Timesheets.Add(newTimesheet);

            await _context.SaveChangesAsync();

            return Ok(newTimesheet);
        }

        [HttpPut]
        //[Authorize]
        public async Task<ActionResult<Employee>> UpdateTimesheet([FromBody] TimesheetDto timesheetDto, int idEmployee, int idTimesheet)
        {
            var employee =await _context.Employees
                .Include (e => e.Timesheets)
                .FirstOrDefaultAsync(e => e.Id == idEmployee);
            if (employee == null)
            {
                return NotFound(new { message = "Employee not found." });
            }

            var timesheetToUpdate = employee.Timesheets.Find(e => e.Id ==idTimesheet);
            
            if (timesheetToUpdate == null)
            {
                return NotFound(new { message = "Timesheet not found for the given employee." });
            }

            timesheetToUpdate.Date = timesheetDto.Date;
            timesheetToUpdate.TimeOfWorking = timesheetDto.TimeOfWorking;

            await _context.SaveChangesAsync();
            return Ok(timesheetToUpdate);
        }
        [HttpDelete]
        //[Authorize]
        public async Task<ActionResult> DeleteTimesheet(int idEmployee, int idTimesheet)
        {
            var employee = await _context.Employees
                .Include(e => e.Timesheets)
                .FirstOrDefaultAsync (e => e.Id == idEmployee);

            if (employee == null)
            {
                    return NotFound(new { message = "Employee not found." });                
            }
            var timesheet = await _context.Timesheets
                .FirstOrDefaultAsync (e => e.Id == idTimesheet);
            if (timesheet == null)
            {
                return NotFound(new { message = "Timesheet not found for the given employee." });
            }
            _context.Timesheets.Remove(timesheet);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
