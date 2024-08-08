using Task = TrackingCalendar.Models.Entity.Task;

namespace TrackingCalendar.Services;

public interface ITaskService
{
    Task<List<Task>> GetTasksAsync();
    Task<Task?> GetTaskAsync(Guid id);
    Task<Task> AddTask(Task task);
    Task<Task> UpdateTask(Task task);
    Task<Guid> DeleteTask(Guid id);
}