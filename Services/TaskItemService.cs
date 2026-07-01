using ProjectService.DTOs;
using ProjectService.Models;
using ProjectService.Repositories;
using ProjectService.Utilities;

namespace ProjectService.Services;

public class TaskItemService: ITaskItemService
{
    private readonly IUserStoryRepository _userStoryRepository;
    private readonly ITaskItemRepository _taskItemRepository;
    private readonly IFeatureRepository _featureRepository;

    public TaskItemService(
        IUserStoryRepository userStoryRepository,
        ITaskItemRepository taskItemRepository,
        IFeatureRepository featureRepository)
    {
        _userStoryRepository = userStoryRepository;
        _taskItemRepository = taskItemRepository;
        _featureRepository = featureRepository;
    }

    public async Task<List<TaskItemResponse>> GetUserStoryTaskItemsAsync(Guid userStoryId)
    {
        var taskItems = await _taskItemRepository.GetByUserStoryIdAsync(userStoryId);
        return taskItems.Select(ti => new TaskItemResponse
        {
            Id = ti.Id,
            UserStoryId = ti.UserStoryId,
            Name = ti.Name,
            Status = ti.Status,
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
            Status = taskItem.Status,
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
            UserStory = userStory,
            Name = request.Name,
            Status = request.Status ?? ProjectTaskStatus.ToDo
        };

        var createdTaskItem = await _taskItemRepository.CreateAsync(taskItem);
        await SyncFeatureStatusAsync(createdTaskItem);

        return new TaskItemResponse
        {
            Id = createdTaskItem.Id,
            UserStoryId = createdTaskItem.UserStoryId,
            Name = createdTaskItem.Name,
            Status = createdTaskItem.Status,
            CreatedAt = createdTaskItem.CreatedAt
        };
    }

    public async Task<TaskItemResponse?> UpdateTaskItemAsync(Guid userStoryId, Guid taskItemId, UpdateTaskItemRequest request)
    {
        var taskItem = await _taskItemRepository.GetByIdAsync(taskItemId);
        if (taskItem == null || taskItem.UserStoryId != userStoryId) return null;

        taskItem.Name = request.Name;
        if (request.Status.HasValue)
        {
            taskItem.Status = request.Status.Value;
        }

        var updatedTaskItem = await _taskItemRepository.UpdateAsync(taskItem);
        if (updatedTaskItem == null) return null;

        await SyncFeatureStatusAsync(updatedTaskItem);

        return new TaskItemResponse
        {
            Id = updatedTaskItem.Id,
            UserStoryId = updatedTaskItem.UserStoryId,
            Name = updatedTaskItem.Name,
            Status = updatedTaskItem.Status,
            CreatedAt = updatedTaskItem.CreatedAt
        };
    }

    public async Task<bool> DeleteTaskItemAsync(Guid userStoryId, Guid taskItemId)
    {
        var taskItem = await _taskItemRepository.GetByIdAsync(taskItemId);
        if (taskItem == null || taskItem.UserStoryId != userStoryId) return false;

        var userStory = await _userStoryRepository.GetByIdAsync(userStoryId);
        var featureIdForSync = userStory?.FeatureId;

        await _taskItemRepository.DeleteAsync(taskItem);

        if (featureIdForSync.HasValue)
        {
            await SyncFeatureStatusFromFeatureIdAsync(featureIdForSync.Value);
        }

        return true;
    }

    private async Task SyncFeatureStatusAsync(TaskItem taskItem)
    {
        var userStory = await _userStoryRepository.GetByIdAsync(taskItem.UserStoryId);
        if (userStory == null) return;

        await SyncFeatureStatusFromFeatureIdAsync(userStory.FeatureId);
    }

    private async Task SyncFeatureStatusFromFeatureIdAsync(Guid featureId)
    {
        var feature = await _featureRepository.GetByIdAsync(featureId);
        if (feature == null) return;

        var taskItems = await _taskItemRepository.GetByFeatureIdAsync(featureId);
        if (!taskItems.Any())
        {
            if (feature.Status != ProjectTaskStatus.ToDo)
            {
                feature.Status = ProjectTaskStatus.ToDo;
                await _featureRepository.UpdateAsync(feature);
            }

            return;
        }

        var derivedStatus = FeatureStatusResolver.Resolve(taskItems);
        if (feature.Status != derivedStatus)
        {
            feature.Status = derivedStatus;
            await _featureRepository.UpdateAsync(feature);
        }
    }
}
