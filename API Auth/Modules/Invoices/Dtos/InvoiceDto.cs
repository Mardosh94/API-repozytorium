using API_Auth.Modules.Invoices.Entities;
using System.ComponentModel.DataAnnotations;

namespace API_Auth.Modules.Suppliers.Dtos
{
    public class InvoiceDto
    {
        [Required(ErrorMessage = "Invoice number is required"), MaxLength(25)]
        public string InvoiceNumber { get; set; }
        [Required(ErrorMessage = "Date is required")]
        [DataType(DataType.Date, ErrorMessage = "Invalid data format.")]
        public DateTime InvoiceDate { get; set; }
        [Required(ErrorMessage = "Due date is required")]
        [DataType(DataType.Date, ErrorMessage = "Invalid data format.")]
        public DateTime DueDate { get; set; }
        [Required(ErrorMessage = "Amount is required")]
        public decimal Amount { get; set; }
        [Required(ErrorMessage = "Answer true or false")]
        public bool IsPayed { get; set; }
        [Required]
        [Range(1, 2)]
        public TypeOfInvoice InvoiceType { get; set; }
        
        [DataType(DataType.Date, ErrorMessage = "Invalid data format.")]
        public DateTime? PaymentDate { get; set; }
    }
}
