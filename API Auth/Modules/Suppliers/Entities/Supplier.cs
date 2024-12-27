using API_Auth.Modules.Employees.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_Auth.Modules.Invoices.Entities
{
    public class Supplier
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int AddressId { get; set; }
        public Address Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public int? invoiceId { get; set; }
        public Invoice Invoices { get; set; }
    }
}
