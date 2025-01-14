using System.ComponentModel.DataAnnotations;

namespace API_Auth.Modules.Customers.Dtos
{
    public class OrderDto
    {
        [Required(ErrorMessage = "Date of order is required")]
        [DataType(DataType.Date, ErrorMessage = "Invalid data format.")]
        public DateTime OrderDate { get; set; }
        public string? Description { get; set; }
    }
}
