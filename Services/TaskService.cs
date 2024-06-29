using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System;
using System.Reflection.Metadata.Ecma335;
using TaskApi.Data;
using TaskApi.DTOs;
using TaskApi.Entity;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TaskApi.Services
{
    public class TaskService : ITaskServices
    {
        private readonly AppDbContext _appDbContext;
        public TaskService(AppDbContext appDbContext)
        { 
            _appDbContext = appDbContext;   
        }
        public async Task<TaskU?> CreateAsync(TaskDto? task)
        {
            var userExists = await _appDbContext.Users.FirstOrDefaultAsync(user => user.Id == task!.UserId);
            if (userExists== null)
            {
                return null;
            }
            var taskU = new TaskU{ User = userExists };
            var entity = _appDbContext.Entry<TaskU>(taskU with
            {
                UserId = task!.UserId,
                Date = task.Date,
                StartTime = task.StartTime,
                EndTime = task.EndTime,
                Description = task.Description,
                Subject = task.Subject

            });
            entity.State = EntityState.Added;
            await _appDbContext.SaveChangesAsync();
            entity.State = EntityState.Detached;
            return entity.Entity;
            //    task with
            //{
            //    UserId = entity.Entity.UserId,
            //    Date = entity.Entity.Date,
            //    StartTime = entity.Entity.StartTime,
            //    EndTime = entity.Entity.EndTime,
            //    Description = entity.Entity.Description,
            //    Subject = entity.Entity.Subject
            //};
        }

        public async Task<Guid> DeleteAsync(Guid id)
        {
            var task = await _appDbContext.Tasks.FirstOrDefaultAsync(task => task.Id == id);
            if (task != null)
            {
               _appDbContext.Tasks.Remove(task);
               await _appDbContext.SaveChangesAsync();
               id = task.Id;
            }
            return id;
        }

        public async Task<List<TaskU>> GetAllTaskAsync() => await
        _appDbContext.Tasks
            .Include(u => u.User)
            .OrderBy(u => u.Date)
            .ThenBy(u => u.StartTime)
            .ToListAsync();
        public async Task<List<TaskU>> GetAllCurrentTaskAsync() => await
       _appDbContext.Tasks
           .Include(u => u.User)
           .Where(u => u.StartTime.Date == DateTime.Today)
           .OrderBy(u => u.StartTime)
           .ToListAsync();

        public async Task<List<TaskU>> GetAllFutureTaskAsync() => await
      _appDbContext.Tasks
          .Include(u => u.User)
          .Where(u => u.StartTime.Date > DateTime.Today)
          .OrderBy(u => u.StartTime)
          .ToListAsync();

        public async Task<TaskU> GetByIdAsync(Guid id)
        {
            var task = await _appDbContext.Tasks.FirstOrDefaultAsync(x => x.Id == id);
            return task!;
        }

        public async Task<TaskU?> UpdateAsync(TaskUpdateDto? task)
        {
            var existingTask = await _appDbContext.Tasks.AsNoTracking().FirstOrDefaultAsync(p => p.Id == task!.Id);
            if (existingTask == null)
            {
                return null;
            }
            var userExists = await _appDbContext.Users.AsNoTracking().FirstOrDefaultAsync(user => user.Id == task!.UserId);
            if (userExists == null)
            {
                return null;
            }

            var entity = _appDbContext.Entry<TaskU>(existingTask with
            {
                UserId = userExists.Id,
                User = userExists,
                Date = task!.Date,
                StartTime = task!.StartTime,
                EndTime = task!.EndTime,
                Description = task!.Description,
                Subject = task!.Subject
            });
            entity.State = EntityState.Modified;
            await _appDbContext.SaveChangesAsync();
            entity.State = EntityState.Detached;
            return entity.Entity;
            
        }
       
        public async Task SaveAsync(TaskU task)
        {
            try
            {
                _appDbContext.Entry(task).State = task.Id == Guid.Empty ? EntityState.Added : EntityState.Modified;

                await _appDbContext.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine("An error occurred while saving changes to the database: " + ex.Message);
            }
        }
    }
}
