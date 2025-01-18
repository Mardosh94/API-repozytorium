using System.ComponentModel.DataAnnotations;

namespace API_Auth.Modules.Employees.Entities
{
    public class Address
    {
        [Key]
        public int Id { get; set; }
        [Required, MaxLength(50)]
        public string City { get; set; }
        [Required, MaxLength(50)]
        public string PostCode { get; set; }
        [MaxLength(50)]
        public string? Street { get; set; }
        [Required, MaxLength(20)]
        public string BuildingNumber { get; set; }
    }
}
