using ProjectService.DTOs;
using ProjectService.Models;

namespace ProjectService.Services;

public interface IProjectService
{
    Task<List<Project>> GetAllProjectsAsync();
    Task<Project?> GetProjectByIdAsync(Guid id);
    Task<ProjectOperationResult<Project>> CreateProjectAsync(CreateProjectRequest request);
    Task<ProjectOperationResult<Project>> UpdateProjectAsync(Guid id, UpdateProjectRequest request);
    Task<ProjectOperationResult> DeleteProjectAsync(Guid id);
}

public enum ProjectOperationStatus
{
    Success,
    NotFound,
    Forbidden
}

public sealed record ProjectOperationResult(ProjectOperationStatus Status)
{
    public static ProjectOperationResult Success() => new(ProjectOperationStatus.Success);
    public static ProjectOperationResult NotFound() => new(ProjectOperationStatus.NotFound);
    public static ProjectOperationResult Forbidden() => new(ProjectOperationStatus.Forbidden);
}

public sealed record ProjectOperationResult<T>(ProjectOperationStatus Status, T? Value)
{
    public static ProjectOperationResult<T> Success(T value) => new(ProjectOperationStatus.Success, value);
    public static ProjectOperationResult<T> NotFound() => new(ProjectOperationStatus.NotFound, default);
    public static ProjectOperationResult<T> Forbidden() => new(ProjectOperationStatus.Forbidden, default);
}
