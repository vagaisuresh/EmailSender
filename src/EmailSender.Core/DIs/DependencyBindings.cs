using EmailSender.Core.Application.Interfaces;
using EmailSender.Core.Application.Services;
using EmailSender.Core.Domain.Repositories;
using EmailSender.Core.Persistence.Repositories;

namespace EmailSender.Core.DIs;

public static class DependencyBindings
{
    public static void RegisterCoreServices(this IServiceCollection services)
    {
        try
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IAccountRepository, AccountRepository>();

            
            services.AddScoped<IContactGroupService, ContactGroupService>();
            services.AddScoped<IContactService, ContactService>();
            services.AddScoped<IEmailService, SmtpEmailService>();

            

            
            services.AddScoped<IContactGroupRepository, ContactGroupRepository>();
            services.AddScoped<IContactRepository, ContactRepository>();
        }
        catch (Exception ex)
        {
            throw new Exception($"Error occurred during DI container configuration: {ex.Message}");
        }
    }
}