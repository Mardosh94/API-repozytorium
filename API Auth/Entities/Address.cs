using API_Auth.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_Auth.Entities
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
