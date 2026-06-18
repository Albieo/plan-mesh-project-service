using ProjectService.Models;

namespace ProjectService.Repositories;

public interface ITaskItemRepository
{
    Task<List<TaskItem>> GetByUserStoryIdAsync(Guid userStoryId);
    Task<List<TaskItem>> GetByFeatureIdAsync(Guid featureId);
    Task<TaskItem?> GetByIdAsync(Guid id);
    Task<TaskItem> CreateAsync(TaskItem taskItem);
    Task<TaskItem?> UpdateAsync(TaskItem taskItem);
    Task DeleteAsync(TaskItem taskItem);
}