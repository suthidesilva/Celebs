using CelebApi.Models;
using System.ComponentModel.DataAnnotations;
using static CelebApi.Validation.ValidationHelper;

namespace CelebApi.Validation;

public static class CelebCreateValidator
{
    public static List<ValidationResult> Validate(CelebCreateDto dto)
    {
        var errors = new List<ValidationResult>();

        ValidateName(dto.Name, errors, required: true);
        ValidateGender(dto.Gender, errors, required: true);
        ValidateRole(dto.Role, errors, required: true);
        ValidateBirthDate(dto.BirthDate, errors);
        ValidateImageUrl(dto.Image, errors);

        return errors;
    }
}
