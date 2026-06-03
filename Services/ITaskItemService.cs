using ProjectService.DTOs;

namespace ProjectService.Services;

public interface ITaskItemService
{
    Task<List<TaskItemResponse>> GetUserStoryTaskItemsAsync(Guid userStoryId);
    Task<TaskItemResponse?> GetTaskItemByIdAsync(Guid userStoryId, Guid id);
    Task<TaskItemResponse?> CreateTaskItemAsync(Guid userStoryId, CreateTaskItemRequest request);
    Task<TaskItemResponse?> UpdateTaskItemAsync(Guid userStoryId, Guid taskItemId, UpdateTaskItemRequest request);
    Task<bool> DeleteTaskItemAsync(Guid userStoryId, Guid taskItemId);
}
