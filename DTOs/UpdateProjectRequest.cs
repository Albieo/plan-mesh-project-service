using System.ComponentModel.DataAnnotations;

namespace ProjectService.DTOs;

public class UpdateProjectRequest : IValidatableObject
{
    [Required]
    [MaxLength(200)]
    public string Name { get; set; } = string.Empty;

    [MaxLength(2000)]
    public string? Description { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (string.IsNullOrWhiteSpace(Name))
            yield return new ValidationResult("Project name is required.", [nameof(Name)]);
    }
}
