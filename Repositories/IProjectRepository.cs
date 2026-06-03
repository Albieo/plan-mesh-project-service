using ProjectService.Models;

namespace ProjectService.Repositories;

public interface IProjectRepository
{
    Task<List<Project>> GetAllAsync();
    Task<Project?> GetByIdAsync(Guid id);
    Task<Project> CreateAsync(Project project);
    Task<Project?> UpdateAsync(Project project);
    Task<bool> DeleteAsync(Guid id);
}
