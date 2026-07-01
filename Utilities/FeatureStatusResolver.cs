using ProjectService.Models;

namespace ProjectService.Utilities;

public static class FeatureStatusResolver
{
    public static ProjectTaskStatus Resolve(IEnumerable<TaskItem> taskItems)
    {
        var tasks = taskItems?.ToList() ?? new List<TaskItem>();

        if (tasks.Count == 0)
        {
            return ProjectTaskStatus.ToDo;
        }

        if (tasks.All(task => task.Status == ProjectTaskStatus.Done))
        {
            return ProjectTaskStatus.Done;
        }

        if (tasks.All(task => task.Status == ProjectTaskStatus.InProgress || task.Status == ProjectTaskStatus.Done))
        {
            return ProjectTaskStatus.InProgress;
        }

        return ProjectTaskStatus.ToDo;
    }
}
