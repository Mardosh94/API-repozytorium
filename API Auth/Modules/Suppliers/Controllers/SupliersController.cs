using API_Auth.Data;
using API_Auth.Modules.Suppliers.Dtos;
using API_Auth.Modules.Suppliers.Services.SupplierServices;
using Microsoft.AspNetCore.Mvc;

namespace API_Auth.Modules.Invoices.Controllers
{
    [ApiController]
    [Route("Suppliers")]

    public class SupliersController : ControllerBase
    {
        private readonly ISupplierService _supplierService;

        public SupliersController(AppDbContext context, ISupplierService supplierService)
        {
            _supplierService = supplierService;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddSupplier([FromBody] SupplierDto supplier)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _supplierService.AddSupplier(supplier);
            if (result.IsSuccess)
                return Created($"Suppliers/{result.Data.Id.ToString()}", result.Data);
            else
                return StatusCode(500, result.ErrorMessege);
        }
    }
}
