using API_Auth.Data;
using API_Auth.Modules.Customers.Dtos;
using API_Auth.Modules.Customers.Entities;
using API_Auth.Modules.Shared;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace API_Auth.Modules.Customers.Services.CustomerServices
{
    public class CustomerService : ICustomerService
    {
        private readonly AppDbContext _context;
        private readonly IUserHelper _userHelper;

        public CustomerService(AppDbContext context, IUserHelper userHelper)
        {
            _context = context;
            _userHelper = userHelper;
        }

        public async Task<ServiceResult<Customer>> AddCustomer(CustomerDto customerDto)
        {
            try
            {
                var customerDb = customerDto.Adapt<Customer>(); // Mapowanie z Dto na db model

                customerDb.UserId =(await _userHelper.GetMyUser()).Id;

                _context.Customers.Add(customerDb);

                await _context.SaveChangesAsync();
                return ServiceResult<Customer>.Success(customerDb);
            }
            catch (Exception ex)
            {
                return ServiceResult<Customer>.Failure(ex.Message);
            }


        }

        public async Task<ServiceResult> DeleteCustomer(int id)
        {
            var customerResult = await GetCustomerFromDb(id);
            if (customerResult == null)
                return ServiceResult.Failure(ErrorMessages.NotFound);
            try
            {
                // Usuwanie powiązanych danych
                if (customerResult.Address != null)
                {
                    _context.Addresses.Remove(customerResult.Address);
                }

                // Usunięcie samego pracownika
                _context.Customers.Remove(customerResult);
                await _context.SaveChangesAsync();

            }
            catch (Exception)
            {
                return ServiceResult.Failure(ErrorMessages.InternalServerError);
            }

            return ServiceResult.Success();
        }
        public async Task<ServiceResult<Customer>> GetCustomerById(int id)
        {
            var customer = await GetCustomerFromDb(id);
            if (customer == null)
                return ServiceResult<Customer>.Failure("Customer not exists.");

            return ServiceResult<Customer>.Success(customer);
        }

        private async Task<Customer?> GetCustomerFromDb(int id)
        {
            return await _context.Customers
            .Include(e => e.Address)
            .FirstOrDefaultAsync(e => e.Id == id);
        }


        public async Task<List<Customer>> GetAllCustomers()
        {
            var userId = (await _userHelper.GetMyUser()).Id;
            var customers = await _context.Customers
            .Where(x => x.UserId == userId)
            .Include(e => e.Address)
            .ToListAsync();
            return customers;
        }

        public async Task<ServiceResult<Customer>> UpdateCustomer(int id, CustomerDto customereDto)
        {
            var customerFromDb = await GetCustomerFromDb(id);
            if (customerFromDb == null)
                return ServiceResult<Customer>.Failure(ErrorMessages.NotFound);

            customerFromDb.CompanyName = customereDto.CompanyName;
            customerFromDb.FirstName = customereDto.FirstName;
            customerFromDb.LastName = customereDto.LastName;
            customerFromDb.Email = customereDto.Email;
            customerFromDb.PhoneNumber = customereDto.PhoneNumber;
            if (customerFromDb.Address != null)
            {
                customerFromDb.Address.City = customereDto.Address.City;
                customerFromDb.Address.PostCode = customereDto.Address.PostCode;
                customerFromDb.Address.Street = customereDto.Address.Street;
                customerFromDb.Address.BuildingNumber = customereDto.Address.BuildingNumber;
            }

            try
            {
                _context.Customers.Update(customerFromDb);
                await _context.SaveChangesAsync();
                return ServiceResult<Customer>.Success(customerFromDb);
            }
            catch (Exception)
            {
                return ServiceResult<Customer>.Failure(ErrorMessages.InternalServerError);
            }
        }
    }
}
