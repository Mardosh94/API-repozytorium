using API_Auth.Data;
using API_Auth.Modules.Invoices.Entities;
using API_Auth.Modules.Shared;
using API_Auth.Modules.Suppliers.Dtos;
using Mapster;

namespace API_Auth.Modules.Suppliers.Services.SupplierServices
{
    public class SupplierService: ISupplierService
    {
        private readonly AppDbContext _context;

        public SupplierService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ServiceResult<Supplier>> AddSupplier(SupplierDto supplier)
        {
            try
            {
                var supplierDb = supplier.Adapt<Supplier>();

                _context.Suppliers.Add(supplierDb);

                await _context.SaveChangesAsync();
                return ServiceResult<Supplier>.Success(supplierDb);
            }
            catch (Exception ex)
            {
                return ServiceResult<Supplier>.Failure(ex.Message);
            }
        }
    }
}
