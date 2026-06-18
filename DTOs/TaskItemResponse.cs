using TaskStatus = ProjectService.Models.TaskStatus;

namespace ProjectService.DTOs;
public class TaskItemResponse
{
    public Guid Id { get; set; }

    public Guid UserStoryId { get; set; }

    public string Name { get; set; } = string.Empty;

    public TaskStatus Status { get; set; }

    public DateTime CreatedAt { get; set; }
}