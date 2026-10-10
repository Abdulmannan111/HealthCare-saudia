using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Entities
{
    public class CreateNotificationRequest
    {
        [Required(ErrorMessage = "Subject is required.")]
        [StringLength(100, ErrorMessage = "Subject cannot be longer than 100 characters.")]
        public string subject { get; set; } = string.Empty;

        [Required(ErrorMessage = "Content is required.")]
       // [StringLength(500, ErrorMessage = "Content cannot be longer than 500 characters.")]
        public string content { get; set; } = string.Empty;

        [Required(ErrorMessage = "IsIndividual is required.")]
        public bool is_individual { get; set; }

        [RequiredIfIndividual(ErrorMessage = "User emails are required when individual.")]
        // [EmailAddress(ErrorMessage = "Each email must be valid.")]
        public List<string>? user_emails { get; set; }

        [RequiredIfNotIndividual(ErrorMessage = "User roles are required when not individual.")]
        public List<string>? user_roles { get; set; }
    }

    // Custom validation attributes for conditional validation based on IsIndividual

    public class RequiredIfIndividualAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object? value, ValidationContext context)
        {
            // Safely access the ObjectInstance
            var target = context?.ObjectInstance as CreateNotificationRequest;

            // Ensure we have the target object and it is correctly cast
            if (target != null)
            {
                // Check if IsIndividual is true
                if (target.is_individual)
                {
                    // Ensure that the value is a List<string> (user_emails) and check if it is null or empty
                    if (value is List<string> userEmails)
                    {
                        if (userEmails.Count == 0)
                        {
                            // Return a validation failure result with an appropriate error message
                            return new ValidationResult("User emails are required when individual.", new[] { context.MemberName });
                        }
                    }
                    else
                    {
                        // Return a validation failure result if the value is not a List<string>
                        return new ValidationResult("Invalid format for user emails.", new[] { context.MemberName });
                    }
                }
            }

            // Validation passes if IsIndividual is false or the list is valid
            return ValidationResult.Success;
        }
    }


    public class RequiredIfNotIndividualAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object? value, ValidationContext context)
        {
            // Safely access the ObjectInstance
            var target = context?.ObjectInstance as CreateNotificationRequest;

            // Ensure we have the target object and it is correctly cast
            if (target != null)
            {
                // Check if IsIndividual is false
                if (!target.is_individual)
                {
                    // Ensure that the value is a List<string> and check if it is null or empty
                    if (value is List<string> userRoles)
                    {
                        if (userRoles.Count == 0)
                        {
                            // Return a validation failure result with an appropriate error message
                            return new ValidationResult("User roles are required when not individual.", new[] { context.MemberName });
                        }
                    }
                    else
                    {
                        // Return a validation failure result if the value is not a List<string>
                        return new ValidationResult("Invalid format for user roles.", new[] { context.MemberName });
                    }
                }
            }

            // Validation passes if IsIndividual is true or the list is valid
            return ValidationResult.Success;
        }
    }
    public class ApiResponse
    {
        public string Data { get; set; } = string.Empty;
        public int StatusCode { get; set; }
    }
}
