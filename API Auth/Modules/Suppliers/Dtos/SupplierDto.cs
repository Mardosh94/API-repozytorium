using System.ComponentModel.DataAnnotations;
using API_Auth.Modules.Employees.Dtos;

namespace API_Auth.Modules.Suppliers.Dtos
{
    public class SupplierDto
    {
        [Required(ErrorMessage = "Name is required"), MaxLength(50, ErrorMessage = "First name cannot exceed 50 characters.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Address is required")]
        public AddressDto Address { get; set; }
        [Required, MaxLength(20, ErrorMessage = "Phone number cannot exceed 20 characters.")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Email name is required")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string Email { get; set; }
    }
}
