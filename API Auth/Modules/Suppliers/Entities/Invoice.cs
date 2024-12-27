using System.ComponentModel.DataAnnotations;

namespace API_Auth.Modules.Invoices.Entities
{
    public class Invoice
    {
        public int Id { get; set; }
        public string InvoiceNumber { get; set; }
        public DateTime InvoiceDate { get; set; }
        public DateTime DueDate { get; set; }
        public decimal Amount { get; set; }
        public bool IsPayed { get; set; }
        public TypeOfInvoice InvoiceType { get; set; }
        public DateTime? PaymentDate { get; set; }
    }

    public enum TypeOfInvoice
    {
        Przychod = 1,
        Koszt = 2
    }
}
