using EmailSender.Core.Application.Interfaces;
using EmailSender.Core.Infrastructure.Services;
using EmailSender.Core.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using NLog;

namespace EmailSender.Core.DIs;

public static class ServiceRegistrations
{
    public static void RegisterSqlContext(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("ConnectionStrings:DefaultConnection");
        services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));
    }

    public static void RegisterLoggerService(this IServiceCollection services)
    {
        LogManager.Setup().LoadConfigurationFromFile(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));
        services.AddSingleton<ILoggerService, LoggerService>();
    }
}