using EmailSender.Core.Domain.Entities;

namespace EmailSender.Core.Application.Interfaces;

public interface IAccountService
{
    Task<IEnumerable<EmailAccount>> GetEmailAccountsAsync();
    Task<EmailAccount?> GetEmailAccountByIdAsync(short id);
    Task<EmailAccount> CreateEmailAccountAsync(EmailAccount account);
    Task UpdateEmailAccountAsync(short id, EmailAccount account);
    Task DeleteEmailAccountAsync(short id);
}