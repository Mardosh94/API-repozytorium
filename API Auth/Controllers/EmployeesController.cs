using API_Auth.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace API_Auth.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public EmployeesController(AppDbContext context)
        {
            _context = context;
        }

        // CREATE: POST /Employees
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Employee>> CreateEmployee([FromBody] Employee employee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Dodanie nowego pracownika
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetEmployee), new { id = employee.Id }, employee);
        }

        // DELETE: DELETE /Employees/{id}
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var employee = await _context.Employees
                .Include(e => e.Address)
                .Include(e => e.Timesheets)
                .FirstOrDefaultAsync(e => e.Id ==id);
                ;
            if (employee == null)
            {
                return NotFound();
            }

            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();

            return NoContent(); // 204 - No Content, ponieważ obiekt został usunięty
        }

        // GET: GET /Employees/{id}
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<Employee>> GetEmployee(int id)
        {
            var employee = await _context.Employees
            .Include(e => e.Address)
            .Include(e => e.Timesheets)// Załaduj powiązaną encję Address.
            .FirstOrDefaultAsync(e => e.Id == id);
            

            if (employee == null)
            {
                return NotFound();
            }
 
                return Ok(employee); // Zwrócenie 200 OK i danych pracownika
           
        }

        // GET: GET /Employees
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Employee>>> GetAllEmployees()
        {
            var employees = await _context.Employees
                .Include(e => e.Address)
                .Include(e => e.Timesheets)
                .ToListAsync();
            return Ok(employees); // Zwrócenie 200 OK i listy pracowników
        }
    }
}
