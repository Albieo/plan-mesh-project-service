using Microsoft.AspNetCore.Mvc;
using ProjectService.DTOs;
using ProjectService.Services;
using Microsoft.AspNetCore.Authorization;

namespace ProjectService.Controllers;

[Authorize]
[ApiController]
[Route("projects")]
public class ProjectsController : ControllerBase
{
    private readonly IProjectService _projectService;

    public ProjectsController(IProjectService projectService)
    {
        _projectService = projectService;
    }

    [HttpGet]
    public async Task<IActionResult> GetProjects()
    {
        var projects = await _projectService.GetAllProjectsAsync();

        var response = projects.Select(project => new ProjectGetResponse
        {
            Id = project.Id,
            Name = project.Name,
            Description = project.Description,
            OwnerUserId = project.OwnerUserId
        });

        return Ok(response);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetProject(Guid id)
    {
        var project = await _projectService.GetProjectByIdAsync(id);

        if (project == null)
            return NotFound();

        var response = new ProjectGetResponse
        {
            Id = project.Id,
            Name = project.Name,
            Description = project.Description,
            OwnerUserId = project.OwnerUserId
        };

        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> CreateProject(CreateProjectRequest request)
    {
        var result = await _projectService.CreateProjectAsync(request);

        return result.Status switch
        {
            ProjectOperationStatus.Success => CreatedAtAction(nameof(GetProject), new { id = result.Value!.Id }, result.Value),
            ProjectOperationStatus.Forbidden => Forbid(),
            _ => StatusCode(StatusCodes.Status500InternalServerError)
        };
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateProject(Guid id, UpdateProjectRequest request)
    {
        var result = await _projectService.UpdateProjectAsync(id, request);

        return result.Status switch
        {
            ProjectOperationStatus.Success => Ok(new ProjectGetResponse
            {
                Id = result.Value!.Id,
                Name = result.Value.Name,
                Description = result.Value.Description,
                OwnerUserId = result.Value.OwnerUserId
            }),
            ProjectOperationStatus.Forbidden => Forbid(),
            ProjectOperationStatus.NotFound => NotFound(),
            _ => StatusCode(StatusCodes.Status500InternalServerError)
        };
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteProject(Guid id)
    {
        var result = await _projectService.DeleteProjectAsync(id);

        return result.Status switch
        {
            ProjectOperationStatus.Success => NoContent(),
            ProjectOperationStatus.Forbidden => Forbid(),
            ProjectOperationStatus.NotFound => NotFound(),
            _ => StatusCode(StatusCodes.Status500InternalServerError)
        };
    }
}
