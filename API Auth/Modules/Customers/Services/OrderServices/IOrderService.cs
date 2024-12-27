using API_Auth.Modules.Customers.Dtos;
using API_Auth.Modules.Customers.Entities;
using API_Auth.Modules.Shared;

namespace API_Auth.Modules.Customers.Services.OrderServices
{
    public interface IOrderService
    {
        Task<ServiceResult<Order>> AddOrder(int customerId, OrderDto orderDto);
        Task<ServiceResult> DeleteOrder(int customerId, int orderId);
        Task<ServiceResult<List<Order>>> GetAllOrdersByCustomerId(int customerId);
    }
}
