using System.ComponentModel.DataAnnotations;
using ProjectService.Models;

namespace ProjectService.DTOs;

public class UpdateFeatureRequest : IValidatableObject
{
    [Required]
    [MaxLength(200)]
    public string Name { get; set; } = string.Empty;

    [MaxLength(2000)]
    public string? Description { get; set; }

    public TaskStatus? Status { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (string.IsNullOrWhiteSpace(Name))
            yield return new ValidationResult("Feature name is required.", [nameof(Name)]);
    }
}
