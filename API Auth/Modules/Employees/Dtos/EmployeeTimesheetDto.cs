namespace API_Auth.Modules.Employees.Dtos
{
    public class EmployeeTimesheetDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public decimal TotalWorkedTime { get; set; }
        public List<TimesheetDto> Timesheets { get; set; }
    }
}
