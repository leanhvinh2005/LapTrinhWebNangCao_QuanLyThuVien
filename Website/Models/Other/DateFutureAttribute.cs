using System.ComponentModel.DataAnnotations;

namespace Website.Models.Custom
{
    public class DateFutureAttribute : ValidationAttribute
    {
        public DateFutureAttribute()
        {
            ErrorMessage = $"Date can't be in the future";
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
                return ValidationResult.Success;

            DateTime birthDate;

            if (value is DateOnly dateOnly)
                birthDate = dateOnly.ToDateTime(TimeOnly.MinValue);
            else if (value is DateTime dt)
                birthDate = dt;
            else
                return new ValidationResult("Invalid date");

            var today = DateTime.Today;

            if (birthDate > today)
                return new ValidationResult("Date can't be in the future.");

            return ValidationResult.Success;
        }
    }
}
