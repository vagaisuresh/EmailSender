using EmailSender.Core.Domain.Entities;

namespace EmailSender.Core.Application.Interfaces;

public interface IEmailAccountService
{
    Task<IEnumerable<EmailAccount>> GetEmailAccountsAsync();
    Task<EmailAccount> GetEmailAccountByIdAsync(short id);
    Task<EmailAccount> CreateEmailAccountAsync(EmailAccount account);
    Task<EmailAccount> UpdateEmailAccountAsync(short id, EmailAccount account);
    Task<EmailAccount> DeleteEmailAccountAsync(short id);
}