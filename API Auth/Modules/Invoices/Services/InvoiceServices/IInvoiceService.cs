using API_Auth.Modules.Invoices.Entities;
using API_Auth.Modules.Shared;
using API_Auth.Modules.Suppliers.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace API_Auth.Modules.Suppliers.Services.InvoiceServices
{
    public interface IInvoiceService
    {
        Task<ServiceResult<Invoice>> AddInvoice(InvoiceDto invoice);
        Task<ServiceResult> DeleteInvoiceById(int id);
        Task<ServiceResult<Invoice>> GetInvoiceById(int id);
        Task<List<Invoice>> GetInvoicesByType(int type);
    }
}
