using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_Auth.Entities
{
    // uzupełnij atrybuty
    public class Employee
    {
        [Key]
        public int Id { get; set; }
        [Required, MaxLength(50)]
        public string FirstName { get; set; }
        [Required, MaxLength(50)]
        public string LastName { get; set; }
        [Required, EmailAddress]
        public string Email { get; set; }
        [Required]
        public DateTime DateOfBirth { get; set; }
        [Required, ForeignKey("Address")]
        public int AddressId { get; set; }

        public Address Address { get; set; }
        public List<Timesheet>? Timesheets { get; set; }
    }
}

