using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using TaskApi.Entity;
namespace TaskApi.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options) : base(options)
    { }


    public DbSet<User> Users { get; set; }
    public DbSet<TaskU> Tasks { get; set; }


    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //{
    //    optionsBuilder.UseSqlServer(config["ConnectionString"], sqlServerOptionsAction: o => o.MigrationsAssembly("TaskApi"));
    //}

    // Example: Creating a database index in EF Core
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Fluent API configurations - it is possible only with DA
        modelBuilder.Entity<TaskU>()
            .Property(e => e.Id)
            .IsRequired();

        modelBuilder.Entity<User>()
            .Property(e => e.Id)
            .IsRequired();

    }
}

