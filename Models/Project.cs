namespace ProjectService.Models;

public class Project
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }

    public Guid OwnerUserId { get; set; }

    public ICollection<Feature> Features { get; set; } = new List<Feature>();

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
