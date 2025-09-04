using System.ComponentModel.DataAnnotations;

namespace CelebApi.Models;

public class CelebCreateDto
{
    [Required]
    public string Name { get; set; }
    public string? BirthDate { get; set; }
    [Required]
    public string Gender { get; set; }
    [Required]
    public string Role { get; set; }
    public string? Image { get; set; }
}