using EmailSender.Core.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace EmailSender.Core.DIs;

public static class ServiceRegistrations
{
    public static void RegisterSqlContext(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("ConnectionStrings:DefaultConnection");
        services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));
    }
}