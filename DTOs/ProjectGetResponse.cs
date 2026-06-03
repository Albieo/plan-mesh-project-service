namespace ProjectService.DTOs;

public class ProjectGetResponse
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }

    public Guid OwnerUserId { get; set; }
}
