using API_Auth.Data;
using API_Auth.Modules.Employees.Dtos;
using API_Auth.Modules.Employees.Entities;
using API_Auth.Modules.Employees.Services.TimesheetServices;
using API_Auth.Modules.Shared;
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
        // POST api/employees/{employeeId}/timesheet
        [HttpPost("/timesheet/{employeeId}")]
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
        }

        [HttpDelete("/timesheet/{employeeId}/{timesheetId}")]
        public async Task<IActionResult> DeleteTimesheet(int employeeId, int timesheetId)
        {
            var deleteResult = await _timesheetService.DeleteTimesheet(employeeId, timesheetId);

            if (!deleteResult.IsSuccess)
            {
                if (deleteResult.ErrorMessege.Equals(ErrorMessages.NotFound))
                    return NotFound();

                if (deleteResult.ErrorMessege.Equals(ErrorMessages.InternalServerError))
                    return StatusCode(500);
            }
            return NoContent(); // 204 - No Content, ponieważ obiekt został usunięty
        }
        [HttpGet("/timesheet/total-hours/{employeeId}")]
        public async Task<IActionResult> GetTotalHours(int employeeId)
        {
            var result = await _timesheetService.GetTotalHours(employeeId);

            if (result.IsSuccess)
            {
                return Ok(new { TotalHours = result});
            }

            if (result.ErrorMessege.Contains("not found"))
            {
                return NotFound(result.ErrorMessege);
            }

            return StatusCode(500, result.ErrorMessege);
        }

    }
}
