using System.ComponentModel.DataAnnotations;
using ProjectService.Models;

namespace ProjectService.DTOs;

public class UpdateTaskItemRequest : IValidatableObject
{
    [Required]
    [MaxLength(200)]
    public string Name { get; set; } = "General";

    public ProjectTaskStatus? Status { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (string.IsNullOrWhiteSpace(Name))
            yield return new ValidationResult("Task name is required.", [nameof(Name)]);
    }
}