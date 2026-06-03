using ProjectService.DTOs;

namespace ProjectService.Services;

public interface IUserStoryService
{
    Task<List<UserStoryResponse>> GetFeatureUserStoriesAsync(Guid featureId);
    Task<UserStoryResponse?> GetUserStoryByIdAsync(Guid featureId, Guid id);
    Task<UserStoryResponse?> CreateUserStoryAsync(Guid featureId, CreateUserStoryRequest request);
    Task<UserStoryResponse?> UpdateUserStoryAsync(Guid featureId, Guid userStoryId, UpdateUserStoryRequest request);
    Task<bool> DeleteUserStoryAsync(Guid featureId, Guid userStoryId);
}
