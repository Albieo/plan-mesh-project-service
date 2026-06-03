using Microsoft.AspNetCore.Mvc;
using ProjectService.DTOs;
using ProjectService.Services;
using Microsoft.AspNetCore.Authorization;

namespace ProjectService.Controllers;

[Authorize]
[ApiController]
[Route("projects/{projectId:guid}/features/{featureId:guid}/user-stories/{userStoryId:guid}/tasks")]
public class TaskItemController : ControllerBase
{
    private readonly ITaskItemService _taskItemService;

    public TaskItemController(ITaskItemService taskItemService)
    {
        _taskItemService = taskItemService;
    }

    [HttpGet]
    public async Task<ActionResult<List<TaskItemResponse>>> GetUserStoryTaskItems(Guid userStoryId)
    {
        var taskItems = await _taskItemService.GetUserStoryTaskItemsAsync(userStoryId);
        return Ok(taskItems);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<TaskItemResponse>> GetTaskItem(Guid userStoryId, Guid id)
    {
        var taskItem = await _taskItemService.GetTaskItemByIdAsync(userStoryId, id);

        if (taskItem == null)
            return NotFound();

        return Ok(taskItem);
    }

    [HttpPost]
    public async Task<ActionResult<TaskItemResponse>> CreateTaskItem(Guid userStoryId, CreateTaskItemRequest request)
    {
        var taskItem = await _taskItemService.CreateTaskItemAsync(userStoryId, request);
        if (taskItem == null) return NotFound("User story not found");
        return CreatedAtAction(nameof(GetTaskItem), new { userStoryId, id = taskItem.Id }, taskItem);
    }

    [HttpPut("{taskItemId:guid}")]
    public async Task<ActionResult<TaskItemResponse>> UpdateTaskItem(Guid userStoryId, Guid taskItemId, UpdateTaskItemRequest request)
    {
        var taskItem = await _taskItemService.UpdateTaskItemAsync(userStoryId, taskItemId, request);
        if (taskItem == null) return NotFound("Task item not found");

        return Ok(taskItem);
    }

    [HttpDelete("{taskItemId:guid}")]
    public async Task<IActionResult> DeleteTaskItem(Guid userStoryId, Guid taskItemId)
    {
        var result = await _taskItemService.DeleteTaskItemAsync(userStoryId, taskItemId);
        if (!result) return NotFound("Task item not found");
        return NoContent();
    }
}