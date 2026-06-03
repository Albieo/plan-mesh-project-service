using ProjectService.Models;

namespace ProjectService.Repositories;

public interface IFeatureRepository
{
    Task<List<Feature>> GetByProjectIdAsync(Guid projectId);
    Task<Feature?> GetByIdAsync(Guid id);
    Task<Feature> CreateAsync(Feature feature);
    Task<Feature?> UpdateAsync(Feature feature);
    Task DeleteAsync(Feature feature);
}