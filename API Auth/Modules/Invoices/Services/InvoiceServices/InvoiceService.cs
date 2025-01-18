using API_Auth.Data;
using API_Auth.Modules.Invoices.Entities;
using API_Auth.Modules.Shared;
using API_Auth.Modules.Suppliers.Dtos;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace API_Auth.Modules.Suppliers.Services.InvoiceServices
{
    public class InvoiceService : IInvoiceService
    {
        private readonly AppDbContext _context;
        private readonly IUserHelper _userHelper;

        public InvoiceService(AppDbContext context, IUserHelper userHelper)
        {
            _context = context;
            _userHelper=userHelper;
        }

        public async Task<ServiceResult<Invoice>> AddInvoice(InvoiceDto invoiceDto)
        {
            try
            {
                var invoiceDb = invoiceDto.Adapt<Invoice>();
                invoiceDb.UserId = (await _userHelper.GetMyUser()).Id;
                _context.Invoices.Add(invoiceDb);
                 await _context.SaveChangesAsync();
                return ServiceResult<Invoice>.Success(invoiceDb);

            }
            catch (Exception ex)
            {
                return ServiceResult<Invoice>.Failure(ex.Message);
            }
        }

        public async Task<ServiceResult> DeleteInvoiceById(int id)
        {
            var invoiceResult = await GetInvoiceFromDb(id);
            if (invoiceResult == null)
                return ServiceResult.Failure(ErrorMessages.NotFound);
            try
            {
                _context.Invoices.Remove(invoiceResult);
                await _context.SaveChangesAsync();

            }
            catch (Exception)
            {
                return ServiceResult.Failure(ErrorMessages.InternalServerError);
            }
            return ServiceResult.Success();
        }

        public async Task<List<Invoice>> GetInvoicesByType(int type)
        {
            var userId = (await _userHelper.GetMyUser()).Id;
            var invoices = await _context.Invoices
                .Where(e => (int)e.InvoiceType == type)
                .Where(x => x.UserId == userId)
                .ToListAsync();
            return invoices;
        }

        public async Task<ServiceResult<Invoice>> GetInvoiceById(int id)
        {
            var invoice = await GetInvoiceFromDb(id);
            if(invoice == null)
                return ServiceResult<Invoice>.Failure("Invoice not exists.");
            return ServiceResult<Invoice>.Success(invoice);
        }

        private async Task<Invoice?> GetInvoiceFromDb(int id)
        {
            return await _context.Invoices
            .FirstOrDefaultAsync(e => e.Id == id);
        }
    }
}
