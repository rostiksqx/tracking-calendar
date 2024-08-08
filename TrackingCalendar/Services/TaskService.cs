using Microsoft.EntityFrameworkCore;
using TrackingCalendar.Data;
using Task = TrackingCalendar.Models.Entity.Task;

namespace TrackingCalendar.Services;

public class TaskService : ITaskService
{
    private readonly ApplicationDbContext _dbContext;

    public TaskService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<List<Task>> GetTasksAsync()
    {
        return await _dbContext.Tasks
            .AsNoTracking()
            .OrderByDescending(t => t.StartDate)
            .ToListAsync();
    }
    
    public async Task<Task?> GetTaskAsync(Guid id)
    {
        var task = await _dbContext.Tasks.FindAsync(id);
        
        return task ?? null;
    }

    public async Task<Task> AddTask(Task task)
    {
        await _dbContext.Tasks.AddAsync(task);
        await _dbContext.SaveChangesAsync();
        
        return task;
    }
    
    public async Task<Task> UpdateTask(Task task)
    {
        await _dbContext.Tasks
            .Where(t => t.Id == task.Id)
            .ExecuteUpdateAsync(s => s.
                SetProperty(t => t.Title, task.Title)
                    .SetProperty(t => t.Description, task.Description)
                    .SetProperty(t => t.StartDate, task.StartDate)
                    .SetProperty(t => t.EndDate, task.EndDate)
                    .SetProperty(t => t.IsCompleted, task.IsCompleted));
        
        return task;
    }
    
    public async Task<Guid> DeleteTask(Guid id)
    {
        await _dbContext.Tasks
            .Where(t => t.Id == id)
            .ExecuteDeleteAsync();
        
        return id;
    }
}