using ProjectService.DTOs;
using ProjectService.Models;
using ProjectService.Repositories;

namespace ProjectService.Services;

public class TaskItemService: ITaskItemService
{
    private readonly IUserStoryRepository _userStoryRepository;
    private readonly ITaskItemRepository _taskItemRepository;

    public TaskItemService(IUserStoryRepository userStoryRepository, ITaskItemRepository taskItemRepository)
    {
        _userStoryRepository = userStoryRepository;
        _taskItemRepository = taskItemRepository;
    }

    public async Task<List<TaskItemResponse>> GetUserStoryTaskItemsAsync(Guid userStoryId)
    {
        var taskItems = await _taskItemRepository.GetByUserStoryIdAsync(userStoryId);
        return taskItems.Select(ti => new TaskItemResponse
        {
            Id = ti.Id,
            UserStoryId = ti.UserStoryId,
            Name = ti.Name,
            CreatedAt = ti.CreatedAt
        }).ToList();
    }

    public async Task<TaskItemResponse?> GetTaskItemByIdAsync(Guid userStoryId, Guid id)
    {
        var taskItem = await _taskItemRepository.GetByIdAsync(id);
        if (taskItem == null || taskItem.UserStoryId != userStoryId) return null;

        return new TaskItemResponse
        {
            Id = taskItem.Id,
            UserStoryId = taskItem.UserStoryId,
            Name = taskItem.Name,
            CreatedAt = taskItem.CreatedAt
        };
    }

    public async Task<TaskItemResponse?> CreateTaskItemAsync(Guid userStoryId, CreateTaskItemRequest request)
    {
        var userStory = await _userStoryRepository.GetByIdAsync(userStoryId);
        if (userStory == null) return null;

        var taskItem = new TaskItem
        {
            UserStoryId = userStoryId,
            Name = request.Name
        };

        var createdTaskItem = await _taskItemRepository.CreateAsync(taskItem);
        return new TaskItemResponse
        {
            Id = createdTaskItem.Id,
            UserStoryId = createdTaskItem.UserStoryId,
            Name = createdTaskItem.Name,
            CreatedAt = createdTaskItem.CreatedAt
        };
    }

    public async Task<TaskItemResponse?> UpdateTaskItemAsync(Guid userStoryId, Guid taskItemId, UpdateTaskItemRequest request)
    {
        var taskItem = await _taskItemRepository.GetByIdAsync(taskItemId);
        if (taskItem == null || taskItem.UserStoryId != userStoryId) return null;

        taskItem.Name = request.Name;

        var updatedTaskItem = await _taskItemRepository.UpdateAsync(taskItem);
        if (updatedTaskItem == null) return null;

        return new TaskItemResponse
        {
            Id = updatedTaskItem.Id,
            UserStoryId = updatedTaskItem.UserStoryId,
            Name = updatedTaskItem.Name,
            CreatedAt = updatedTaskItem.CreatedAt
        };
    }

    public async Task<bool> DeleteTaskItemAsync(Guid userStoryId, Guid taskItemId)
    {
        var taskItem = await _taskItemRepository.GetByIdAsync(taskItemId);
        if (taskItem == null || taskItem.UserStoryId != userStoryId) return false;

        await _taskItemRepository.DeleteAsync(taskItem);

        return true;
    }
}
