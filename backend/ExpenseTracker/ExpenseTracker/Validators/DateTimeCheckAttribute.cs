using System.ComponentModel.DataAnnotations;

namespace ExpenseTracker.Validators
{
    public class DateTimeCheckAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var date = (DateTime?)value;

            if(date > DateTime.Now)
            {
                return new ValidationResult("Expense date should be today or a previous day");
            }

            return ValidationResult.Success;
        }
    }
}
