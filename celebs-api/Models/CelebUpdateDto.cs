namespace CelebApi.Models;

public class CelebUpdateDto
{
    public string? Name { get; set; }
    public string? BirthDate { get; set; }
    public string? Gender { get; set; }
    public List<string>? Roles { get; set; }
    public string? Image { get; set; }
}