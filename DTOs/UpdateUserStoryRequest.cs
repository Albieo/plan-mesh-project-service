using System.ComponentModel.DataAnnotations;

namespace ProjectService.DTOs;

public class UpdateUserStoryRequest : IValidatableObject
{
    [Required]
    [MaxLength(200)]
    public string Name { get; set; } = "General";

    [MaxLength(2000)]
    public string? Description { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (string.IsNullOrWhiteSpace(Name))
            yield return new ValidationResult("User story name is required.", [nameof(Name)]);
    }
}
