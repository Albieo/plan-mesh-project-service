using ProjectService.DTOs;
using ProjectService.Models;
using ProjectService.Repositories;

namespace ProjectService.Services;

public class ProjectsService : IProjectService
{
    private readonly IProjectRepository _projectRepository;
    private readonly ICurrentUserService _currentUserService;

    public ProjectsService(IProjectRepository projectRepository, ICurrentUserService currentUserService)
    {
        _projectRepository = projectRepository;
        _currentUserService = currentUserService;
    }

    public async Task<List<Project>> GetAllProjectsAsync()
    {
        return await _projectRepository.GetAllAsync();
    }

    public async Task<Project?> GetProjectByIdAsync(Guid id)
    {
        return await _projectRepository.GetByIdAsync(id);
    }

    public async Task<ProjectOperationResult<Project>> CreateProjectAsync(CreateProjectRequest request)
    {
        if (_currentUserService.UserId is not Guid userId)
            return ProjectOperationResult<Project>.Forbidden();

        var project = new Project
        {
            Id = Guid.NewGuid(),
            Name = request.Name.Trim(),
            Description = string.IsNullOrWhiteSpace(request.Description) ? null : request.Description.Trim(),
            OwnerUserId = userId,
            CreatedAt = DateTime.UtcNow
        };

        var createdProject = await _projectRepository.CreateAsync(project);

        return ProjectOperationResult<Project>.Success(createdProject);
    }

    public async Task<ProjectOperationResult<Project>> UpdateProjectAsync(Guid id, UpdateProjectRequest request)
    {
        var project = await _projectRepository.GetByIdAsync(id);

        if (project == null)
            return ProjectOperationResult<Project>.NotFound();

        if (!IsCurrentUserProjectOwner(project))
            return ProjectOperationResult<Project>.Forbidden();

        project.Name = request.Name.Trim();
        project.Description = string.IsNullOrWhiteSpace(request.Description) ? null : request.Description.Trim();

        var updatedProject = await _projectRepository.UpdateAsync(project);

        return ProjectOperationResult<Project>.Success(updatedProject!);
    }

    public async Task<ProjectOperationResult> DeleteProjectAsync(Guid id)
    {
        var project = await _projectRepository.GetByIdAsync(id);

        if (project == null)
            return ProjectOperationResult.NotFound();

        if (!IsCurrentUserProjectOwner(project))
            return ProjectOperationResult.Forbidden();

        await _projectRepository.DeleteAsync(id);

        return ProjectOperationResult.Success();
    }

    private bool IsCurrentUserProjectOwner(Project project)
    {
        return _currentUserService.UserId is Guid userId && project.OwnerUserId == userId;
    }
}
