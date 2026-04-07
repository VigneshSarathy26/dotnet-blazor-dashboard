using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DotNetBlazorDashboard.Infrastructure.Extensions;

public class DashboardDbContext : DbContext
{
    public DashboardDbContext(DbContextOptions<DashboardDbContext> options) : base(options) { }
}

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var dbProvider = configuration.GetValue<string>("DatabaseProvider") ?? "PostgreSQL";
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        // NOTE: In .NET 10 Preview, ensure correct provider packages are installed.
        // Uncomment the following logic once the provider packages (Npgsql/SqlServer) are resolved.
        
        services.AddDbContext<DashboardDbContext>(options =>
        {
            // if (dbProvider.Equals("SqlServer", StringComparison.OrdinalIgnoreCase))
            // {
            //     options.UseSqlServer(connectionString);
            // }
            // else
            // {
            //     options.UseNpgsql(connectionString);
            // }
            
            options.UseInMemoryDatabase("DashboardDb"); // Temporary for initial scaffolding
        });

        return services;
    }
}
