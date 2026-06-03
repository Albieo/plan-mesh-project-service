using Microsoft.EntityFrameworkCore;
using ProjectService.Data;
using ProjectService.Models;

namespace ProjectService.Repositories;

public class TaskItemRepository : ITaskItemRepository
{
    private readonly AppDbContext _context;

    public TaskItemRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<TaskItem>> GetByUserStoryIdAsync(Guid userStoryId)
    {
        return await _context.TaskItems.Where(ti => ti.UserStoryId == userStoryId).ToListAsync();
    }

    public async Task<TaskItem?> GetByIdAsync(Guid id)
    {
        return await _context.TaskItems.FindAsync(id);
    }

    public async Task<TaskItem> CreateAsync(TaskItem taskItem)
    {
        _context.TaskItems.Add(taskItem);
        await _context.SaveChangesAsync();

        return taskItem;
    }

    public async Task<TaskItem?> UpdateAsync(TaskItem taskItem)
    {
        _context.TaskItems.Update(taskItem);
        await _context.SaveChangesAsync();

        return taskItem;
    }

    public async Task DeleteAsync(TaskItem taskItem)
    {
        _context.TaskItems.Remove(taskItem);
        await _context.SaveChangesAsync();
    }
}