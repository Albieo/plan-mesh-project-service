using System.ComponentModel.DataAnnotations;
namespace ProjectService.Models;

public class Feature
{
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }

    [Required]
    public Guid ProjectId { get; set; }

    public Project Project { get; set; } = null!;

    public ICollection<UserStory> UserStories { get; set; } = new List<UserStory>();
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
