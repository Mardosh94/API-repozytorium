using API_Auth.Data;
using API_Auth.Modules.Employees.Dtos;
using API_Auth.Modules.Employees.Entities;
using API_Auth.Modules.Employees.Responses;
using API_Auth.Modules.Employees.Services.EmployeeServices;
using API_Auth.Modules.Shared;
using Mapster;
using Microsoft.AspNetCore.Mvc;

namespace API_Auth.Modules.Employees.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeesController(AppDbContext context, IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        // CREATE: POST /Employees
        [HttpPost("add")]
        //[Authorize]
        public async Task<IActionResult> AddEmployee([FromBody] EmployeeDto employee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _employeeService.AddEmployee(employee);

            if(result.IsSuccess)
                return Created($"Employees/{result.Data.Id.ToString()}", result.Data);
            else 
                return  StatusCode(500, result.ErrorMessege);
        }

        //DELETE: DELETE /Employees/{id}
        [HttpDelete("delete/{id}")]
        //[Authorize]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var deleteResult = await _employeeService.DeleteEmployee(id);
            if (!deleteResult.IsSuccess)
            {
                if (deleteResult.ErrorMessege.Equals(ErrorMessages.NotFound))
                    return NotFound();

                if (deleteResult.ErrorMessege.Equals(ErrorMessages.InternalServerError))
                    return StatusCode(500);
            }
            return NoContent();
        }

        [HttpPut("update")]
        //[Authorize]
        public async Task<IActionResult> UpdateEmployee([FromBody] EmployeeDto employee, [FromQuery] int id)    ///Do posprzątania
        {
           var updateResult =await _employeeService.UpdateEmployee(id, employee);

            if (updateResult.IsSuccess)
                return Ok(updateResult.Data.Adapt<EmployeeUpdateResponse>());
            else if (updateResult.ErrorMessege.Equals(ErrorMessages.NotFound))
                return NotFound();
           
            return StatusCode(500);
        }
        // GET: GET /Employees/{id}
        [HttpGet("getBy/{id}")]
        //[Authorize]
        public async Task<ActionResult<Employee>> GetEmployeeById(int id)
        {
            var employee = await _employeeService.GetEmployeeById(id);

            if (employee.IsSuccess)
                return Ok(employee.Data);// Zwrócenie 200 OK i danych pracownika
            return NotFound(); 

        }

        // GET: GET /Employees
        [HttpGet("getAll")]
        //[Authorize]
        public async Task<ActionResult<IEnumerable<Employee>>> GetAllEmployees()
        {
            var employees = await _employeeService.GetAllEmployees();
            return Ok(employees); // Zwrócenie 200 OK i listy pracowników
        }
    }
}
