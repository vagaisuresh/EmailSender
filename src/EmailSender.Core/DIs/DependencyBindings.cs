using EmailSender.Core.Application.Interfaces;
using EmailSender.Core.Application.Services;
using EmailSender.Core.Domain.Repositories;
using EmailSender.Core.Persistence.Repositories;

namespace EmailSender.Core.DIs;

public static class DependencyBindings
{
    public static void RegisterCoreServices(this IServiceCollection services)
    {
        services.AddScoped<IEmailService, SmtpEmailService>();

        services.AddScoped<IEmailAccountService, EmailAccountService>();
        services.AddScoped<IEmailAccountRepository, EmailAccountRepository>();
    }
}