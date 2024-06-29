using EFCore.AutomaticMigrations;
using TaskApi.Data;

namespace TaskApi.Extension
{
    public static class DbExtencion
    {
        public static async void CreateDbIfNotExists(this IHost host)
        {
            using var scope = host.Services.CreateScope();
            var services = scope.ServiceProvider;   
            var context = services.GetRequiredService<AppDbContext>();
            context.Database.EnsureCreated();
            context.MigrateToLatestVersion();
           // MigrateDatabaseToLatestVersion.Execute(context, new DbMigrationsOptions {ResetDatabaseSchema=true,} );   
            DbSeedData.InitDataBase(context);
        }
    }
}
