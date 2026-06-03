using System.ComponentModel.DataAnnotations;
namespace ProjectService.Models;

public class UserStory
{
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    public string Name { get; set; } = "General";

    public string? Description { get; set; }

    [Required]
    public Guid FeatureId { get; set; }

    public Feature Feature { get; set; } = null!;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}