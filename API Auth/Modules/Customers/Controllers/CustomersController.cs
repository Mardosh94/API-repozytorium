using API_Auth.Data;
using API_Auth.Modules.Customers.Dtos;
using API_Auth.Modules.Customers.Entities;
using API_Auth.Modules.Customers.Responses;
using API_Auth.Modules.Customers.Services.CustomerServices;
using API_Auth.Modules.Shared;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API_Auth.Modules.Customers.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomersController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpPost("add")]
        [Authorize]
        public async Task<IActionResult> AddCustomer([FromBody] CustomerDto customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _customerService.AddCustomer(customer);

            if (result.IsSuccess)
                return Created($"Employees/{result.Data.Id.ToString()}", result.Data);
            else
                return StatusCode(500, result.ErrorMessege);
        }

        //DELETE: DELETE /Customer/{id}
        [HttpDelete("delete/{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            var deleteResult = await _customerService.DeleteCustomer(id);
            if (!deleteResult.IsSuccess)
            {
                if (deleteResult.ErrorMessege.Equals(ErrorMessages.NotFound))
                    return NotFound();

                if (deleteResult.ErrorMessege.Equals(ErrorMessages.InternalServerError))
                    return StatusCode(500);
            }
            return NoContent(); // 204 - No Content, ponieważ obiekt został usunięty
        }

        [HttpPut("update")]
        [Authorize]
        public async Task<IActionResult> UpdateCustomer([FromBody] CustomerDto customer, [FromQuery] int id)    ///Do posprzątania
        {
            var updateResult = await _customerService.UpdateCustomer(id, customer);

            if (updateResult.IsSuccess)
                return Ok(updateResult.Data.Adapt<CustomerUpdateResponse>());
            else if (updateResult.ErrorMessege.Equals(ErrorMessages.NotFound))
                return NotFound();

            return StatusCode(500);
        }
        // GET: GET /Customer/{id}
        [HttpGet("getBy/{id}")]
        [Authorize]
        public async Task<ActionResult<Customer>> GetCustomerById(int id)
        {
            var customer = await _customerService.GetCustomerById(id);

            if (customer.IsSuccess)
                return Ok(customer.Data);// Zwrócenie 200 OK i danych pracownika
            return NotFound();

        }

        // GET: GET /Customer
        [HttpGet("getAll")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Customer>>> GetAllCustomer()
        {
            var customer = await _customerService.GetAllCustomers();
            return Ok(customer); // Zwrócenie 200 OK i listy pracowników
        }
    }
}

