using System.ComponentModel.DataAnnotations;

namespace Website.Models.Custom
{
    public class BirthDateAttribute : ValidationAttribute
    {
        private readonly int _minAge;

        public BirthDateAttribute(int minAge)
        {
            _minAge = minAge;
            ErrorMessage = $"Date can't be in the future and age must be {_minAge}+ years old";
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

            var age = today.Year - birthDate.Year;
            if (birthDate > today.AddYears(-age)) age--; 

            if (age < _minAge)
                return new ValidationResult($"Must be {_minAge}+ years old.");

            return ValidationResult.Success;
        }
    }
}
