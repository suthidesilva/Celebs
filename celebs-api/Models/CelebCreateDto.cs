using System.ComponentModel.DataAnnotations;

namespace CelebApi.Models;

public class CelebCreateDto
{
    [Required]
    public required string Name { get; set; }
    public string? BirthDate { get; set; }
    [Required]
    public required string Gender { get; set; }
    [Required]
    public required List<string> Roles { get; set; } = new List<string>();
    public string? Image { get; set; }
}