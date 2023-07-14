using Microsoft.EntityFrameworkCore;
using PalitronicaDemo.Model;

namespace PalitronicaDemo.Infrastrucure
{
    public static class MigrateDbContextService 
    {
        public static IApplicationBuilder UseMigrationService(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var serviceProvider = scope.ServiceProvider;
            var dbContext = serviceProvider.GetService<MyDbContext>();

            try
            {
                dbContext.Database.Migrate();
            }
            catch (Exception ex)
            {
                // Handle migration error if necessary
            }

            return app;
        }
    }
}
