using API_Auth.Data;
using API_Auth.Modules.Customers.Dtos;
using API_Auth.Modules.Customers.Entities;
using API_Auth.Modules.Shared;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace API_Auth.Modules.Customers.Services.OrderServices
{
    public class OrderService : IOrderService
    {
        private readonly AppDbContext _context;

        public OrderService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<ServiceResult<Order>> AddOrder(int customerId, OrderDto orderDto)
        {
            try
            {
                var customer = await GetCustomerFromDb(customerId);
                if (customer == null)
                {
                    return ServiceResult<Order>.Failure(ErrorMessages.NotFound);
                }

                var order = orderDto.Adapt<Order>();
                customer.Orders ??= new List<Order>();
                customer.Orders.Add(order);

                _context.Orders.Add(order);

                await _context.SaveChangesAsync();
                return ServiceResult<Order>.Success(order);
            }
            catch (Exception)
            {
                return ServiceResult<Order>.Failure(ErrorMessages.InternalServerError);
            }
        }

        public async Task<ServiceResult> DeleteOrder(int customerId, int orderId)
        {
            try
            {
                var customer = await GetCustomerFromDb(customerId);
                if (customer == null || !customer.Orders.Any(t => t.Id == orderId))
                {
                    return ServiceResult<Customer>.Failure(ErrorMessages.NotFound);
                }
                var order = customer.Orders.First(t => t.Id == orderId);
                _context.Orders.Remove(order);

                await _context.SaveChangesAsync();
                return ServiceResult.Success();
            }
            catch
            {
                return ServiceResult<Order>.Failure(ErrorMessages.InternalServerError);
            }
        }

        public async Task<ServiceResult<List<Order>>> GetAllOrdersByCustomerId(int customerId)
        {
            try
            {
                var customer = await GetCustomerFromDb(customerId);
                if (customer == null || !customer.Orders.Any())
                {
                    return ServiceResult<List<Order>>.Failure(ErrorMessages.NotFound);
                }

                return ServiceResult<List<Order>>.Success(customer.Orders);
            }
            catch (Exception)
            {
                return ServiceResult<List<Order>>.Failure(ErrorMessages.InternalServerError);
            }
        }

        private async Task<Customer?> GetCustomerFromDb(int id)
        {
            return await _context.Customers
            .Include(e => e.Orders)
            .FirstOrDefaultAsync(e => e.Id == id);
        }
    }
}
