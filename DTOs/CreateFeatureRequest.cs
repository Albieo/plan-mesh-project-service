namespace ProjectService.DTOs;

public class CreateFeatureRequest
{
    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }
}