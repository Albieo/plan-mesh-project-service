using Microsoft.EntityFrameworkCore;
using ProjectService.Data;
using ProjectService.Models;

namespace ProjectService.Repositories;

public class FeatureRepository : IFeatureRepository
{
    private readonly AppDbContext _context;

    public FeatureRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Feature>> GetByProjectIdAsync(Guid projectId)
    {
        return await _context.Features.Where(f => f.ProjectId == projectId).ToListAsync();
    }

    public async Task<Feature?> GetByIdAsync(Guid id)
    {
        return await _context.Features.FindAsync(id);
    }

    public async Task<Feature> CreateAsync(Feature feature)
    {
        _context.Features.Add(feature);
        await _context.SaveChangesAsync();
        
        return feature;
    }

    public async Task<Feature?> UpdateAsync(Feature feature)
    {
        _context.Features.Update(feature);
        await _context.SaveChangesAsync();

        return feature;
    }

    public async Task DeleteAsync(Feature feature)
    {
        _context.Features.Remove(feature);
        await _context.SaveChangesAsync();
    }
}