using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using PalitronicaDemo.Model;

namespace PalitronicaDemo.Infrastrucure
{
    public static class DbContextConfig
    {
        
        public static void AddDbContext(this IServiceCollection services, IConfiguration configuration)
        {
             
        services.AddDbContext<MyDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("MyTestDbConn"));
            });

        }
    }
}
