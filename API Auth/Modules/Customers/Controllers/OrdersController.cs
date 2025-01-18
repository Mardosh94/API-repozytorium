using API_Auth.Modules.Customers.Dtos;
using API_Auth.Modules.Customers.Services.OrderServices;
using API_Auth.Modules.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API_Auth.Modules.Customers.Controllers
{
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost("/Customers/{customerId}/Orders")]
        [Authorize]
        public async Task<IActionResult> AddOrder(int customerId, [FromBody] OrderDto orderDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _orderService.AddOrder(customerId, orderDto);

            if (result.IsSuccess)
                return Created($"Customers/{customerId}/Orders/{result.Data.Id}", result.Data);
            else
                return StatusCode(500, result.ErrorMessege);
        }

        [HttpDelete("/Customers/{customerId}/Orders/{orderId}")]
        [Authorize]
        public async Task<IActionResult> DeleteOrder(int customerId, int orderId)
        {
            var deleteResult = await _orderService.DeleteOrder(customerId, orderId);

            if (!deleteResult.IsSuccess)
            {
                if (deleteResult.ErrorMessege.Equals(ErrorMessages.NotFound))
                    return NotFound();
                return StatusCode(500);
            }
            return NoContent(); // 204 - No Content, ponieważ obiekt został usunięty
        }
        [HttpGet("/Customers/{customerId}/Orders")]
        [Authorize]
        public async Task<IActionResult> GetAllOrdersByCustomerId(int customerId)
        {
            var result = await _orderService.GetAllOrdersByCustomerId(customerId);

            if (!result.IsSuccess)
                return NotFound();

            return Ok(result.Data);

        }
    }
}
