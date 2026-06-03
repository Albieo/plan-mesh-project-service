using System.ComponentModel.DataAnnotations;
namespace ProjectService.Models;

public class TaskItem
{
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    public string Name { get; set; } = "General";

    [Required]
    public Guid UserStoryId { get; set; }

    public UserStory UserStory { get; set; } = null!;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}