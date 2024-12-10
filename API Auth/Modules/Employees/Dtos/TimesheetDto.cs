using System.ComponentModel.DataAnnotations;

namespace API_Auth.Modules.Employees.Dtos
{
    public class TimesheetDto
    {
        public DateTime Date { get; set; }
        public decimal TimeOfWorking { get; set; }
    }
}
