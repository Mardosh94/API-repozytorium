using API_Auth.Modules.Employees.Entities;

namespace API_Auth.Modules.Customers.Responses
{
    public class CustomerUpdateResponse
    {
        public int Id { get; set; }

        public string CompanyName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public int AddressId { get; set; }
        public Address Address { get; set; }
    }
}
