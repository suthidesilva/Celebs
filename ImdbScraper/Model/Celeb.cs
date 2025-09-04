using ImdbScraper.Model.Enum;

namespace ImdbScraper.Model;

public class Celeb
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = "";
    public List<CelebRole> Roles { get; set; } = []; 
    public string? Image { get; set; }
    public string ProfileUrl { get; set; } = "";
    public string? BirthDate { get; set; }
    public string Gender { get; set; } = "Unknown";
}
