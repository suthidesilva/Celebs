using CelebApi.Models;
using System.ComponentModel.DataAnnotations;
using static CelebApi.Validation.ValidationHelper;

namespace CelebApi.Validation;

public static class CelebUpdateValidator
{
    public static List<ValidationResult> Validate(CelebUpdateDto dto)
    {
        var errors = new List<ValidationResult>();

        ValidateName(dto.Name, errors, required: false);
        ValidateGender(dto.Gender, errors, required: false);
        ValidateRoles(dto.Roles, errors, required: false);
        ValidateBirthDate(dto.BirthDate, errors);
        ValidateImageUrl(dto.Image, errors);

        return errors;
    }
}
