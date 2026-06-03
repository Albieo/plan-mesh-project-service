using System.ComponentModel.DataAnnotations;

namespace ProjectService.DTOs;

public class UpdateTaskItemRequest : IValidatableObject
{
    [Required]
    [MaxLength(200)]
    public string Name { get; set; } = "General";

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (string.IsNullOrWhiteSpace(Name))
            yield return new ValidationResult("Task name is required.", [nameof(Name)]);
    }
}
