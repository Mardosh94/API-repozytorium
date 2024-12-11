using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_Auth.Modules.Invoices
{
    public class Invoice
    {
        [Key]
        public int Id { get; set; }
        [Required, MaxLength(25)]
        public string InvoiceNumber { get; set; }
        [Required]
        public DateTime InvoiceDate { get; set; }
        [Required]
        public DateTime DueDate { get; set; }
        [Required]
        public decimal Amount { get; set; }
        [Required]
        public bool IsPayed { get; set; }
        [Required]
        public TypeOfInvoice InvoiceType { get; set; }
        public DateTime? PaymentDate { get; set; }


    }

    public enum TypeOfInvoice
    {
        Przychod = 1,
        Koszt = 2
    }
}
