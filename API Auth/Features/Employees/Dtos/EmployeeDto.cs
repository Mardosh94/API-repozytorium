using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using API_Auth.Entities;

namespace API_Auth.Features.Employees.Dtos
{
    public class EmployeeDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }
        public AddressDto Address { get; set; }
        public List<TimesheetDto>? Timesheets { get; set; }
    }
}
