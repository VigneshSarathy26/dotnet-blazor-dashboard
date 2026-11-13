namespace DotNetBlazorDashboard.Infrastructure.Data;

using Microsoft.EntityFrameworkCore;

public class DashboardDbContext : DbContext
{
    public DashboardDbContext(DbContextOptions<DashboardDbContext> options)
        : base(options)
    {
    }
}

