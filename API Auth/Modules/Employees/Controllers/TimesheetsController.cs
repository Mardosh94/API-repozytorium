using API_Auth.Modules.Employees.Dtos;
using API_Auth.Modules.Employees.Services.TimesheetServices;
using API_Auth.Modules.Shared;
using Microsoft.AspNetCore.Mvc;

namespace API_Auth.Modules.Employees.Controllers
{
    [ApiController]
    public class TimesheetsController : ControllerBase
    {
        private readonly ITimesheetService _timesheetService;

        public TimesheetsController(ITimesheetService timesheetService)
        {
            _timesheetService = timesheetService;
        }

        [HttpPost("/Employees/{employeeId}/Timesheets")]
        public async Task<IActionResult> AddTimesheet(int employeeId, [FromBody] TimesheetDto timesheetDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _timesheetService.AddTimesheet(employeeId, timesheetDto);

            if (result.IsSuccess)
                return Created($"Employees/{employeeId}/Timesheets/{result.Data.Id}", result.Data);
            else
                return StatusCode(500, result.ErrorMessege);
        }

        [HttpDelete("/Employees/{employeeId}/Timesheets/{timesheetId}")]
        public async Task<IActionResult> DeleteTimesheet(int employeeId, int timesheetId)
        {
            var deleteResult = await _timesheetService.DeleteTimesheet(employeeId, timesheetId);

            if (!deleteResult.IsSuccess)
            {
                if (deleteResult.ErrorMessege.Equals(ErrorMessages.NotFound))
                    return NotFound();
                return StatusCode(500);
            }
            return NoContent(); // 204 - No Content, ponieważ obiekt został usunięty
        }
        [HttpGet("/Timesheet/total-hours/{employeeId}")]
        public async Task<IActionResult> GetTotalHours(int employeeId)
        {
            var result = await _timesheetService.GetTotalHours(employeeId);

            if (result.IsSuccess)
            {
                return Ok(new { TotalHours = result});
            }

            if (result.ErrorMessege.Equals(ErrorMessages.NotFound))
            {
                return NotFound();
            }

            return StatusCode(500, result.ErrorMessege);
        }
        [HttpGet("/Employees/{employeeId}/Timesheets")]
        public async Task<IActionResult> GetAllTimesheetByEmployeeId(int employeeId)
        {
            var result = await _timesheetService.GetAllTimesheetByEmployeeId(employeeId);

            if (!result.IsSuccess) 
                return NotFound();
                
            return Ok(result.Data);
           
        }

    }
}
