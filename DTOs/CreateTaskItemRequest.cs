using TaskStatus = ProjectService.Models.TaskStatus;

namespace ProjectService.DTOs;

public class CreateTaskItemRequest
{
    public string Name { get; set; } = "General";

    public TaskStatus? Status { get; set; }
}