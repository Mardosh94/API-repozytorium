using API_Auth.Data;
using API_Auth.Modules.Invoices.Entities;
using API_Auth.Modules.Shared;
using API_Auth.Modules.Suppliers.Dtos;
using API_Auth.Modules.Suppliers.Services.InvoiceServices;
using Microsoft.AspNetCore.Mvc;

namespace API_Auth.Modules.Invoices.Controllers
{
    [ApiController]
    [Route("Invoices")]
    public class InvoicesController : ControllerBase
    {
        private readonly IInvoiceService _invoiceService;

        public InvoicesController(AppDbContext context, IInvoiceService invoiceService)
        {
            _invoiceService = invoiceService;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddInvoice([FromBody] InvoiceDto invoice) 
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result= await _invoiceService.AddInvoice(invoice);

            if (result.IsSuccess)
                return Created($"Invoices/{result.Data.Id.ToString()}", result.Data);
            else
                return StatusCode(500, result.ErrorMessege);
        }
        [HttpDelete("delete/{id}")]
        public  async Task<IActionResult> DeleteInvoiceById (int id)
        {
            var deleteResult = await _invoiceService.DeleteInvoiceById(id);
            if (!deleteResult.IsSuccess)
            {
                if (deleteResult.ErrorMessege.Equals(ErrorMessages.NotFound))
                    return NotFound();

                if (deleteResult.ErrorMessege.Equals(ErrorMessages.InternalServerError))
                    return StatusCode(500);
            }
            return NoContent();
        }
        [HttpGet("getById/{id}")]
        public async Task<ActionResult> GetInvoiceById(int id)
        {
            var invoice = await _invoiceService.GetInvoiceById(id);

            if(invoice.IsSuccess)
                return Ok(invoice.Data);
            return NotFound();
        }
        [HttpGet("getByType/{type}")]
        public async Task<ActionResult<IEnumerable<Invoice>>> GetInvoicesByType(int type)
        {
            var invoices = await _invoiceService.GetInvoicesByType(type);
            return Ok(invoices);
        }
    }
}
