using System;
using TaskApi.DTOs;
using TaskApi.Entity;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TaskApi.Data
{
    public class DbSeedData
    {
        public static void InitDataBase(AppDbContext context)
        {
            if (context.Tasks.Any() && context.Users.Any())
                return;
            var userId1 = Guid.NewGuid();
            var user1 = new User
            {
                Id = userId1,
                Name = "Petar"
            };
            var userId2 = Guid.NewGuid();
            var user2 = new User
            {
                Id = userId2,
                Name = "Natalia"
            };

            context.Add(user1);
            context.Add(user2);

            var task1 = new TaskU { Id = Guid.NewGuid(), UserId = userId1,User= user1, Date = DateTime.UtcNow, StartTime = DateTime.UtcNow, EndTime = DateTime.UtcNow.AddDays(3), Subject = "TaskPetarTask1", Description = "Task for Petar 3 days" };
            var task2 = new TaskU { Id = Guid.NewGuid(), UserId = userId1, User = user1, Date = DateTime.UtcNow, StartTime = DateTime.UtcNow.AddDays(1), EndTime = DateTime.UtcNow.AddDays(4), Subject = "TaskPetarTask2", Description = "Task for Petar 4 days" };
            var task3 = new TaskU { Id = Guid.NewGuid(), UserId = userId2, User = user2, Date = DateTime.UtcNow, StartTime = DateTime.UtcNow.AddDays(1), EndTime = DateTime.UtcNow.AddDays(2), Subject = "TaskNataliaTask2", Description = "Task for Natalie 2 days" };
            context.Add(task1);
            context.Add(task2);
            context.Add(task3);
            
            context.SaveChanges();

        }
    }
}
