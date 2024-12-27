using API_Auth.Modules.Customers.Dtos;
using API_Auth.Modules.Customers.Entities;
using API_Auth.Modules.Shared;

namespace API_Auth.Modules.Customers.Services.CustomerServices
{
    public interface ICustomerService
    {
        Task<ServiceResult<Customer>> AddCustomer(CustomerDto customer);
        Task<ServiceResult> DeleteCustomer(int id);
        Task<ServiceResult<Customer>> GetCustomerById(int id);
        Task<ServiceResult<Customer>> UpdateCustomer(int id, CustomerDto customerDto);
        Task<List<Customer>> GetAllCustomers();
    }
}
