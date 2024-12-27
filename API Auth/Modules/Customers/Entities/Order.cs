using API_Auth.Modules.Invoices.Entities;
using System.ComponentModel.DataAnnotations;

namespace API_Auth.Modules.Customers.Entities
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public DateTime OrderDate { get; set; }
        public string? Description { get; set; }
        [Required]
        public bool Status { get; set; }
    }
}