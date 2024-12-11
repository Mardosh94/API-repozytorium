using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_Auth.Modules.Employees.Entities
{
    // uzupełnij atrybuty
    public class Employee
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public DateTime DateOfBirth { get; set; }

        public int AddressId { get; set; }


        public Address Address { get; set; }

        public List<Timesheet>? Timesheets { get; set; }
    }
}

