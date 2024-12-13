using System.ComponentModel.DataAnnotations;

namespace API_Auth.Modules.Employees.Dtos
{
    public class TimesheetDto
    {
        [Required(ErrorMessage = "Date of work is required.")]
        [DataType(DataType.Date, ErrorMessage = "Invalid date format.")]
        [CustomValidation(typeof(TimesheetValidator), nameof(TimesheetValidator.ValidateDate))]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Time of working is required.")]
        [Range(0, 24, ErrorMessage = "Time of working must be between 0 and 24 hours.")]
        public decimal TimeOfWorking { get; set; }
    }

    public static class TimesheetValidator
    {
        public static ValidationResult ValidateDate(DateTime date, ValidationContext context)
        {
            DateTime today = DateTime.Now.Date;
            DateTime oneMonthAgo = today.AddMonths(-1);

            if (date > today)
            {
                return new ValidationResult("Date cannot be in the future.");
            }
            if (date < oneMonthAgo)
            {
                return new ValidationResult("Date cannot be earlier than one month ago.");
            }

            return ValidationResult.Success;
        }
    }
}
