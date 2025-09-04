using System;

namespace CelebApi.Models
{
    public class Celeb
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string? Name { get; set; }
        public string? BirthDate { get; set; }
        public string? Gender { get; set; }
        public string? Role { get; set; }
        public string? Image { get; set; }
    }
}
