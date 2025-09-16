using System;

namespace CelebApi.Models
{
    public class Celeb
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string? Name { get; set; }
        public string? BirthDate { get; set; }
        public string? Gender { get; set; }
        public List<string> Roles { get; set; } = new List<string>();
        public string? Image { get; set; }
    }
}
