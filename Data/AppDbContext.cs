using Microsoft.EntityFrameworkCore;
using ProjectService.Models;

namespace ProjectService.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Project> Projects => Set<Project>();
    public DbSet<Feature> Features => Set<Feature>();
    public DbSet<UserStory> UserStories => Set<UserStory>();
    public DbSet<TaskItem> TaskItems => Set<TaskItem>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Project>()
            .HasMany(p => p.Features)
            .WithOne(f => f.Project)
            .HasForeignKey(f => f.ProjectId)
            .OnDelete(DeleteBehavior.Cascade);
        modelBuilder.Entity<Feature>()
            .HasMany(f => f.UserStories)
            .WithOne(us => us.Feature)
            .HasForeignKey(us => us.FeatureId)
            .OnDelete(DeleteBehavior.Cascade);
        modelBuilder.Entity<UserStory>()
            .HasMany(us => us.TaskItems)
            .WithOne(ti => ti.UserStory)
            .HasForeignKey(ti => ti.UserStoryId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
