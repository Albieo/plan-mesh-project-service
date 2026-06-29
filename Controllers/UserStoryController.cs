using Microsoft.AspNetCore.Mvc;
using ProjectService.DTOs;
using ProjectService.Services;
using Microsoft.AspNetCore.Authorization;

namespace ProjectService.Controllers;

[Authorize]
[ApiController]
[Route("projects/{projectId:guid}/features/{featureId:guid}/user-stories")]
public class UserStoryController : ControllerBase
{
    private readonly IUserStoryService _userStoryService;

    public UserStoryController(IUserStoryService userStoryService)
    {
        _userStoryService = userStoryService;
    }

    [HttpGet]
    public async Task<ActionResult<List<UserStoryResponse>>> GetFeatureUserStories(Guid featureId)
    {
        var userStories = await _userStoryService.GetFeatureUserStoriesAsync(featureId);
        return Ok(userStories);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<UserStoryResponse>> GetUserStory(Guid featureId, Guid id)
    {
        var userStory = await _userStoryService.GetUserStoryByIdAsync(featureId, id);

        if (userStory == null)
            return NotFound();

        return Ok(userStory);
    }

    [HttpPost]
    public async Task<ActionResult<UserStoryResponse>> CreateUserStory(Guid projectId, Guid featureId, CreateUserStoryRequest request)
    {
        var userStory = await _userStoryService.CreateUserStoryAsync(featureId, request);
        if (userStory == null) return NotFound("Feature not found");
        return CreatedAtAction(nameof(GetUserStory), new { projectId, featureId, id = userStory.Id }, userStory);
    }

    [HttpPut("{userStoryId:guid}")]
    public async Task<ActionResult<UserStoryResponse>> UpdateUserStory(Guid featureId, Guid userStoryId, UpdateUserStoryRequest request)
    {
        var userStory = await _userStoryService.UpdateUserStoryAsync(featureId, userStoryId, request);
        if (userStory == null) return NotFound("User story not found");

        return Ok(userStory);
    }

    [HttpDelete("{userStoryId:guid}")]
    public async Task<IActionResult> DeleteUserStory(Guid featureId, Guid userStoryId)
    {
        var result = await _userStoryService.DeleteUserStoryAsync(featureId, userStoryId);
        if (!result) return NotFound("User story not found");
        return NoContent();
    }
}