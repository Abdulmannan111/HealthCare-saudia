using Microsoft.Extensions.DependencyInjection;
using Application.IRepositories;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

public class UniqueEmailAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value == null || string.IsNullOrEmpty(value.ToString()))
        {
            return ValidationResult.Success; // Skip the validation if null or empty (other annotations will catch this)
        }

        string email = value.ToString();

        // Resolve IUserRepository from the IServiceProvider available in the ValidationContext
        var userRepository = (IUserRepository)validationContext.GetService(typeof(IUserRepository));
        var errors = new Dictionary<string, string[]>
{
    { "email", new[] { "The email has already been taken." } }
};
        // Check if the email exists
        var existingUser = userRepository.GetUserByEmailAsync(email).Result;
        if (existingUser != null)
        {
            throw new ValidationException("The email has already been taken.")
            {
                Data = { { "errors", errors } }
            };
        }

        return ValidationResult.Success;
    }
}
