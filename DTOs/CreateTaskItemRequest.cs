using ProjectService.Models;

namespace ProjectService.DTOs;

public class CreateTaskItemRequest
{
    public string Name { get; set; } = "General";

    public ProjectTaskStatus? Status { get; set; }
}