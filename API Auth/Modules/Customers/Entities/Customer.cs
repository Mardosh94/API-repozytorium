using API_Auth.Modules.Employees.Entities;
using API_Auth.Modules.Invoices.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_Auth.Modules.Customers.Entities
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }
        [Required, MaxLength(50)]
        public string CompanyName { get; set; }
        [Required, MaxLength(50)]
        public string FirstName { get; set; }
        [Required, MaxLength(50)]
        public string LastName { get; set; }
        [Required, MaxLength(50), EmailAddress]
        public string Email { get; set; }
        [Required, MaxLength(30)]
        public string PhoneNumber { get; set; }
        [ForeignKey("Address")]
        public int AddressId { get; set; }
        public Address Address { get; set; }

        //public List<Invoice> Invoices { get; set; }

        public List<Order>? Orders { get; set; }
    }
}
