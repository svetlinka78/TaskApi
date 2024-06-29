using TaskApi.DTOs;
using TaskApi.Entity;

namespace TaskApi.Services;

public interface ITaskServices
{
    Task<List<TaskU>> GetAllTaskAsync();
    Task<List<TaskU>> GetAllCurrentTaskAsync();
    Task<List<TaskU>> GetAllFutureTaskAsync();
    Task<TaskU> GetByIdAsync(Guid id);
    Task<TaskU?> CreateAsync(TaskDto? task);
    Task<TaskU?> UpdateAsync(TaskUpdateDto? task);
    Task SaveAsync(TaskU task);
    Task<Guid> DeleteAsync(Guid id);
}

