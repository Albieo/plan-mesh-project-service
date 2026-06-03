using ProjectService.DTOs;

namespace ProjectService.Services;

public interface IFeatureService
{
    Task<List<FeatureResponse>> GetProjectFeaturesAsync(Guid projectId);
    Task<FeatureResponse?> GetFeatureByIdAsync(Guid id);
    Task<FeatureResponse?> CreateFeatureAsync(Guid projectId, CreateFeatureRequest request);
    Task<FeatureResponse?> UpdateFeatureAsync(Guid projectId, Guid featureId, UpdateFeatureRequest request);
    Task<bool> DeleteFeatureAsync(Guid projectId, Guid featureId);
}
