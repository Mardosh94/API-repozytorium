using API_Auth.Entities;
using API_Auth.Features.Employees.Dtos;
using API_Auth.Features.Employees.Services.EmployeeServices;
using Microsoft.AspNetCore.Mvc;

namespace API_Auth.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeesController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IEmployeeService _employeeService;

        public EmployeesController(AppDbContext context, IEmployeeService employeeService)
        {
            _context = context;
            _employeeService = employeeService;

        }

        // CREATE: POST /Employees
        [HttpPost]
        //[Authorize]
        public async Task<ActionResult<Employee>> AddEmployee([FromBody] EmployeeDto employee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result =await _employeeService.AddEmployee(employee);

            return CreatedAtAction(nameof(GetEmployeeById), new { id = result.Id }, result);
        }

        //DELETE: DELETE /Employees/{id}
        [HttpDelete("{id}")]
        //[Authorize]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var employee = await _employeeService.GetEmployeeById(id);
            if (employee == null) 
            {
            return NotFound();
            }
            bool idDelated = await _employeeService.DeleteEmployee(id);
            if (!idDelated) 
            {
                return Conflict();// 409 - Żądanie nie może być zakończone z powodu konfliktu z aktualnym stanem zasobu.
             }
                return NoContent(); // 204 - No Content, ponieważ obiekt został usunięty
        }




        //CO tutaj zrobić??!?!!?

        [HttpPut]
        //[Authorize]
        public async Task<IActionResult> UpdateEmployee([FromBody] EmployeeDto employee, [FromQuery] int id)    ///Do posprzątania
        {
            var employeeToUpdate = await _employeeService.GetEmployeeById(id);
            if(employeeToUpdate == null)
            {
                return NotFound();
            }
            employeeToUpdate.FirstName = employee.FirstName;
            employeeToUpdate.LastName = employee.LastName;
            employeeToUpdate.Email = employee.Email;
            employee.DateOfBirth = employee.DateOfBirth;
            if (employeeToUpdate.Address != null)
            {
                employeeToUpdate.Address.City = employeeToUpdate.Address.City;
                employeeToUpdate.Address.PostCode = employeeToUpdate.Address.PostCode;
                employeeToUpdate.Address.Street = employeeToUpdate.Address.Street;
                employeeToUpdate.Address.BuildingNumber = employeeToUpdate.Address.BuildingNumber;
            }
            await _context.SaveChangesAsync();
            return Ok(employeeToUpdate);
        }






        // GET: GET /Employees/{id}
        [HttpGet("{id}")]
        //[Authorize]
        public async Task<ActionResult<Employee>> GetEmployeeById(int id)
        {
            var employee = await _employeeService.GetEmployeeById(id);
            if (employee == null)
            {
                return NotFound();
            }
 
                return Ok(employee); // Zwrócenie 200 OK i danych pracownika
           
        }

        // GET: GET /Employees
        [HttpGet]
        //[Authorize]
        public async Task<ActionResult<IEnumerable<Employee>>> GetAllEmployees()
        {
            var employees = await _employeeService.GetAllEmployees();
            return Ok(employees); // Zwrócenie 200 OK i listy pracowników
        }
    }
}
