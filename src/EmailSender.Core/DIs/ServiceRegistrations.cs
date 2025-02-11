using EmailSender.Core.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace EmailSender.Core.DIs;

public static class ServiceRegistrations
{
    public static void RegisterSqlContext(this IServiceCollection services)
    {
        services.AddDbContext<AppDbContext>(Options => Options.UseSqlServer("connectionString"));
    }
}