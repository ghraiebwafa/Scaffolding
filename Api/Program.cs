using DataAccess;
using Microsoft.EntityFrameworkCore;

namespace Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddDbContext<MyDbContext>(options =>
        {
            
            options.UseNpgsql(builder.Configuration.GetConnectionString("MyDbConn")); //The connection string is in the appsettings file
        });

        builder.Services.AddControllers();
        
        var app = builder.Build();

 
        
        using (var scope = app.Services.CreateScope())
        {
           var db= scope.ServiceProvider.GetService<MyDbContext>()!
                .Database.EnsureCreated();
        }
        
        app.MapControllers();

        app.Run();
    }
}