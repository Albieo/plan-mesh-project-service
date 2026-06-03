using Microsoft.EntityFrameworkCore;
using ProjectService.Data;
using ProjectService.Models;

namespace ProjectService.Repositories;

public class UserStoryRepository : IUserStoryRepository
{
    private readonly AppDbContext _context;

    public UserStoryRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<UserStory>> GetByFeatureIdAsync(Guid featureId)
    {
        return await _context.UserStories.Where(us => us.FeatureId == featureId).ToListAsync();
    }

    public async Task<UserStory?> GetByIdAsync(Guid id)
    {
        return await _context.UserStories.FindAsync(id);
    }

    public async Task<UserStory> CreateAsync(UserStory userStory)
    {
        _context.UserStories.Add(userStory);
        await _context.SaveChangesAsync();

        return userStory;
    }

    public async Task<UserStory?> UpdateAsync(UserStory userStory)
    {
        _context.UserStories.Update(userStory);
        await _context.SaveChangesAsync();

        return userStory;
    }

    public async Task DeleteAsync(UserStory userStory)
    {
        _context.UserStories.Remove(userStory);
        await _context.SaveChangesAsync();
    }
}