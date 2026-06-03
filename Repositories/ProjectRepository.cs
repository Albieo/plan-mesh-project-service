using Microsoft.EntityFrameworkCore;
using ProjectService.Data;
using ProjectService.Models;

namespace ProjectService.Repositories;

public class ProjectRepository : IProjectRepository
{
    private readonly AppDbContext _context;

    public ProjectRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Project>> GetAllAsync()
    {
        return await _context.Projects.ToListAsync();
    }

    public async Task<Project?> GetByIdAsync(Guid id)
    {
        return await _context.Projects.FindAsync(id);
    }

    public async Task<Project> CreateAsync(Project project)
    {
        _context.Projects.Add(project);
        await _context.SaveChangesAsync();

        return project;
    }

    public async Task<Project?> UpdateAsync(Project project)
    {
        _context.Projects.Update(project);
        await _context.SaveChangesAsync();

        return project;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var project = await _context.Projects.FindAsync(id);

        if (project == null)
            return false;

        _context.Projects.Remove(project);
        await _context.SaveChangesAsync();

        return true;
    }
}
