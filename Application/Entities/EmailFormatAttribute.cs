
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Application.Entities
{
    public class EmailFormatAttribute : ValidationAttribute
    {
        private static readonly Regex EmailRegex = new Regex(
      @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
      RegexOptions.IgnoreCase);

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null && !EmailRegex.IsMatch(value.ToString()))
            {
                return new ValidationResult("Invalid email format.");
            }

            return ValidationResult.Success;
        }
    }
}