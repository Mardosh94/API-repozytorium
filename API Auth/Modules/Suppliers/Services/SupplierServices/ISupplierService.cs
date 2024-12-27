using API_Auth.Modules.Invoices.Entities;
using API_Auth.Modules.Shared;
using API_Auth.Modules.Suppliers.Dtos;

namespace API_Auth.Modules.Suppliers.Services.SupplierServices
{
    public interface ISupplierService
    {
        Task<ServiceResult<Supplier>> AddSupplier(SupplierDto supplier);
    }
}
