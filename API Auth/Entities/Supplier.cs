using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_Auth.Entities
{
    public class Supplier
    {
        [Key]
        public int Id { get; set; }
        [Required, MaxLength(50)]
        public string Name { get; set; }
        [Required, ForeignKey("Address")]
        public int AddressId { get; set; }
        public Address Address { get; set; }
        [Required, MaxLength(30)]
        public string PhoneNumber { get; set; }
        [Required, MaxLength(50), EmailAddress]
        public string Email { get; set; }

        public int? invoiceId { get; set; }
        public Invoice Invoices { get; set; }

    }
}
