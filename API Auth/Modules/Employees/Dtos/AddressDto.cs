using System.ComponentModel.DataAnnotations;

namespace API_Auth.Modules.Employees.Dtos
{
    public class AddressDto
    {
        public string City { get; set; }
        public string PostCode { get; set; }
        public string? Street { get; set; }
        public string BuildingNumber { get; set; }
    }
}
