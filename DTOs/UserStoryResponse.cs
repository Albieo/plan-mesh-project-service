namespace ProjectService.DTOs;
public class UserStoryResponse
{
    public Guid Id { get; set; }

    public Guid FeatureId { get; set; }

    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }

    public DateTime CreatedAt { get; set; }
}