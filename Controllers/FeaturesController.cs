using Microsoft.AspNetCore.Mvc;
using ProjectService.DTOs;
using ProjectService.Services;
using Microsoft.AspNetCore.Authorization;

namespace ProjectService.Controllers;

[Authorize]
[ApiController]
[Route("projects/{projectId:guid}/features")]
public class FeatureController : ControllerBase
{
    private readonly IFeatureService _featureService;

    public FeatureController(IFeatureService featureService)
    {
        _featureService = featureService;
    }

    [HttpGet]
    public async Task<ActionResult<List<FeatureGetResponse>>> GetProjectFeatures(Guid projectId)
    {
        var features = await _featureService.GetProjectFeaturesAsync(projectId);
        var response = features.Select(feature => new FeatureGetResponse
        {
            Id = feature.Id,
            ProjectId = feature.ProjectId,
            Name = feature.Name,
            Description = feature.Description
        }).ToList();

        return Ok(response);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<FeatureGetResponse>> GetFeature(Guid id)
    {
        var feature = await _featureService.GetFeatureByIdAsync(id);

        if (feature == null)
            return NotFound();

        var response = new FeatureGetResponse
        {
            Id = feature.Id,
            ProjectId = feature.ProjectId,
            Name = feature.Name,
            Description = feature.Description
        };

        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<FeatureResponse>> CreateFeature(Guid projectId, CreateFeatureRequest request)
    {
        var feature = await _featureService.CreateFeatureAsync(projectId, request);
        if (feature == null) return NotFound("Project not found");
        return CreatedAtAction(nameof(GetProjectFeatures), new { projectId }, feature);
    }

    [HttpPut("{featureId:guid}")]
    public async Task<ActionResult<FeatureGetResponse>> UpdateFeature(Guid projectId, Guid featureId, UpdateFeatureRequest request)
    {
        var feature = await _featureService.UpdateFeatureAsync(projectId, featureId, request);
        if (feature == null) return NotFound("Feature not found");

        var response = new FeatureGetResponse
        {
            Id = feature.Id,
            ProjectId = feature.ProjectId,
            Name = feature.Name,
            Description = feature.Description
        };

        return Ok(response);
    }

    [HttpDelete("{featureId:guid}")]
    public async Task<IActionResult> DeleteFeature(Guid projectId, Guid featureId)
    {
        var result = await _featureService.DeleteFeatureAsync(projectId, featureId);
        if (!result) return NotFound("Feature not found");
        return NoContent();
    }
}
