

using System.ComponentModel.DataAnnotations;

namespace API_Auth.Modules.Employees.Dtos
{
    public class EmployeeDto
    {
        [Required(ErrorMessage ="First name is required")]
        [StringLength(50, ErrorMessage ="First name cannot exceed 50 characters.")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last name is required")]
        [StringLength(50, ErrorMessage = "Last name cannot exceed 50 characters.")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Email name is required")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Phone number is required")]
        [StringLength(20, ErrorMessage = "Phone number cannot exceed 20 characters.")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage ="Date of birth is required")]
        [DataType(DataType.Date, ErrorMessage = "Invalid data format.")]
        public DateTime DateOfBirth { get; set; }
        [Required(ErrorMessage = "Address is required")]
        public AddressDto Address { get; set; }
    }
}
