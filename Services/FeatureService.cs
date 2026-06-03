using ProjectService.DTOs;
using ProjectService.Models;
using ProjectService.Repositories;

namespace ProjectService.Services;

public class FeatureService: IFeatureService
{
    private readonly IFeatureRepository _featureRepository;
    private readonly IProjectRepository _projectRepository;

    public FeatureService(IFeatureRepository featureRepository, IProjectRepository projectRepository)
    {
        _featureRepository = featureRepository;
        _projectRepository = projectRepository;
    }

    public async Task<List<FeatureResponse>> GetProjectFeaturesAsync(Guid projectId)
    {
        var features = await _featureRepository.GetByProjectIdAsync(projectId);
        return features.Select(f => new FeatureResponse
        {
            Id = f.Id,
            ProjectId = f.ProjectId,
            Name = f.Name,
            Description = f.Description
        }).ToList();
    }

    public async Task<FeatureResponse?> GetFeatureByIdAsync(Guid id)
    {
        var feature = await _featureRepository.GetByIdAsync(id);
        if (feature == null) return null;

        return new FeatureResponse
        {
            Id = feature.Id,
            ProjectId = feature.ProjectId,
            Name = feature.Name,
            Description = feature.Description
        };
    }

    public async Task<FeatureResponse?> CreateFeatureAsync(Guid projectId, CreateFeatureRequest request)
    {
        var project = await _projectRepository.GetByIdAsync(projectId);
        if (project == null) return null;

        var feature = new Feature
        {
            ProjectId = projectId,
            Name = request.Name,
            Description = request.Description
        };

        var createdFeature = await _featureRepository.CreateAsync(feature);
        return new FeatureResponse
        {
            Id = createdFeature.Id,
            ProjectId = createdFeature.ProjectId,
            Name = createdFeature.Name,
            Description = createdFeature.Description,
            CreatedAt = createdFeature.CreatedAt
        };
    }

    public async Task<FeatureResponse?> UpdateFeatureAsync(Guid projectId, Guid featureId, UpdateFeatureRequest request)
    {
        var feature = await _featureRepository.GetByIdAsync(featureId);
        if (feature == null || feature.ProjectId != projectId) return null;

        feature.Name = request.Name;
        feature.Description = request.Description;

        var updatedFeature = await _featureRepository.UpdateAsync(feature);
        if (updatedFeature == null) return null;

        return new FeatureResponse
        {
            Id = feature.Id,
            ProjectId = feature.ProjectId,
            Name = updatedFeature.Name,
            Description = updatedFeature.Description,
            CreatedAt = feature.CreatedAt
        };
    }

    public async Task<bool> DeleteFeatureAsync(Guid projectId, Guid featureId)
    {
        var feature = await _featureRepository.GetByIdAsync(featureId);
        if (feature == null || feature.ProjectId != projectId) return false;

        await _featureRepository.DeleteAsync(feature);

        return true;
    }
}
