namespace ProjectService.DTOs;

public class CreateUserStoryRequest
{
    public string Name { get; set; } = "General";

    public string? Description { get; set; }
}