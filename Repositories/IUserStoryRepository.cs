using ProjectService.Models;

namespace ProjectService.Repositories;

public interface IUserStoryRepository
{
    Task<List<UserStory>> GetByFeatureIdAsync(Guid featureId);
    Task<UserStory?> GetByIdAsync(Guid id);
    Task<UserStory> CreateAsync(UserStory userStory);
    Task<UserStory?> UpdateAsync(UserStory userStory);
    Task DeleteAsync(UserStory userStory);
}