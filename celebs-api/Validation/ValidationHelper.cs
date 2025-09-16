using System.ComponentModel.DataAnnotations;

namespace CelebApi.Validation;

public static class ValidationHelper
{
    public static void ValidateName(string? name, List<ValidationResult> errors, bool required = true)
    {
        if (required && string.IsNullOrWhiteSpace(name))
        {
            errors.Add(new ValidationResult("Name is required.", new[] { "Name" }));
            return;
        }

        if (!string.IsNullOrWhiteSpace(name) && name.Length < 2)
        {
            errors.Add(new ValidationResult("Name must be at least 2 characters.", new[] { "Name" }));
        }
    }

    public static void ValidateGender(string? gender, List<ValidationResult> errors, bool required = true)
    {
        if (required && string.IsNullOrWhiteSpace(gender))
        {
            errors.Add(new ValidationResult("Gender is required.", new[] { "Gender" }));
            return;
        }

        if (!string.IsNullOrWhiteSpace(gender))
        {
            var lower = gender.ToLower();
            if (lower != "male" && lower != "female")
            {
                errors.Add(new ValidationResult("Gender must be 'Male' or 'Female'", new[] { "Gender" }));
            }
        }
    }

    public static void ValidateBirthDate(string? birthDate, List<ValidationResult> errors)
    {
        if (string.IsNullOrWhiteSpace(birthDate)) return;

        if (!DateTime.TryParse(birthDate, out var parsed))
        {
            errors.Add(new ValidationResult("BirthDate must be a valid date", new[] { "BirthDate" }));
        }
        else if (parsed > DateTime.UtcNow)
        {
            errors.Add(new ValidationResult("BirthDate cannot be in the future", new[] { "BirthDate" }));
        }
    }

    public static void ValidateImageUrl(string? image, List<ValidationResult> errors)
    {
        if (string.IsNullOrWhiteSpace(image)) return;

        var isValid = Uri.TryCreate(image, UriKind.Absolute, out _) &&
                      (image.StartsWith("http://") || image.StartsWith("https://"));

        if (!isValid)
        {
            errors.Add(new ValidationResult("Image must be a valid URL", new[] { "Image" }));
        }
    }

    public static void ValidateRole(string? role, List<ValidationResult> errors, bool required = true)
    {
        if (required && string.IsNullOrWhiteSpace(role))
        {
            errors.Add(new ValidationResult("Role is required.", new[] { "Role" }));
        }
    }

    public static void ValidateRoles(List<string>? roles, List<ValidationResult> errors, bool required = true)
    {
        if (required && (roles == null || roles.Count == 0))
        {
            errors.Add(new ValidationResult("At least one role is required.", new[] { "Roles" }));
            return;
        }

        if (roles != null)
        {
            var emptyRoles = roles.Where(r => string.IsNullOrWhiteSpace(r)).ToList();
            if (emptyRoles.Any())
            {
                errors.Add(new ValidationResult("Roles cannot be empty.", new[] { "Roles" }));
            }
        }
    }
}
