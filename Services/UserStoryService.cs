using ProjectService.DTOs;
using ProjectService.Models;
using ProjectService.Repositories;

namespace ProjectService.Services;

public class UserStoryService: IUserStoryService
{
    private readonly IFeatureRepository _featureRepository;
    private readonly IUserStoryRepository _userStoryRepository;

    public UserStoryService(IFeatureRepository featureRepository, IUserStoryRepository userStoryRepository)
    {
        _featureRepository = featureRepository;
        _userStoryRepository = userStoryRepository;
    }

    public async Task<List<UserStoryResponse>> GetFeatureUserStoriesAsync(Guid featureId)
    {
        var userStories = await _userStoryRepository.GetByFeatureIdAsync(featureId);
        return userStories.Select(us => new UserStoryResponse
        {
            Id = us.Id,
            FeatureId = us.FeatureId,
            Name = us.Name,
            Description = us.Description,
            CreatedAt = us.CreatedAt
        }).ToList();
    }

    public async Task<UserStoryResponse?> GetUserStoryByIdAsync(Guid featureId, Guid id)
    {
        var userStory = await _userStoryRepository.GetByIdAsync(id);
        if (userStory == null || userStory.FeatureId != featureId) return null;

        return new UserStoryResponse
        {
            Id = userStory.Id,
            FeatureId = userStory.FeatureId,
            Name = userStory.Name,
            Description = userStory.Description,
            CreatedAt = userStory.CreatedAt
        };
    }

    public async Task<UserStoryResponse?> CreateUserStoryAsync(Guid featureId, CreateUserStoryRequest request)
    {
        var feature = await _featureRepository.GetByIdAsync(featureId);
        if (feature == null) return null;

        var userStory = new UserStory
        {
            FeatureId = featureId,
            Name = request.Name,
            Description = request.Description
        };

        var createdUserStory = await _userStoryRepository.CreateAsync(userStory);
        return new UserStoryResponse
        {
            Id = createdUserStory.Id,
            FeatureId = createdUserStory.FeatureId,
            Name = createdUserStory.Name,
            Description = createdUserStory.Description,
            CreatedAt = createdUserStory.CreatedAt
        };
    }

    public async Task<UserStoryResponse?> UpdateUserStoryAsync(Guid featureId, Guid userStoryId, UpdateUserStoryRequest request)
    {
        var userStory = await _userStoryRepository.GetByIdAsync(userStoryId);
        if (userStory == null || userStory.FeatureId != featureId) return null;
    
        userStory.Name = request.Name;
        userStory.Description = request.Description;

        var updatedUserStory = await _userStoryRepository.UpdateAsync(userStory);
        if (updatedUserStory == null) return null;

        return new UserStoryResponse
        {
            Id = updatedUserStory.Id,
            FeatureId = updatedUserStory.FeatureId,
            Name = updatedUserStory.Name,
            Description = updatedUserStory.Description,
            CreatedAt = updatedUserStory.CreatedAt
        };
    }

    public async Task<bool> DeleteUserStoryAsync(Guid featureId, Guid userStoryId)
    {
        var userStory = await _userStoryRepository.GetByIdAsync(userStoryId);
        if (userStory == null || userStory.FeatureId != featureId) return false;

        await _userStoryRepository.DeleteAsync(userStory);

        return true;
    }
}
