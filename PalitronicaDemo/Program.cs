using Microsoft.EntityFrameworkCore;
using PalitronicaDemo.Infrastrucure;
using PalitronicaDemo.Model;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext(builder.Configuration);

builder.Services.AddControllersWithViews();

builder.Services.AddHttpClient();

var app = builder.Build();
//app.UseMigrationService();

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

//app.UseMiddleware<MigrationServiceMiddleware>(); // Register the migration service middleware here

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
//app.UseMiddleware<TokenValidationMiddleware>();


app.Run();
public class MigrationServiceMiddleware
{
    private readonly RequestDelegate _next;

    public MigrationServiceMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, MyDbContext dbContext)
    {
        // Apply database migrations and ensure the database is up-to-date
        dbContext.Database.Migrate();

        await _next(context);
    }
}