using EmailSender.Core.Domain.Entities;

namespace EmailSender.Core.Interfaces;

public interface IEmailAccountService
{
    Task<IEnumerable<EmailAccount>> GetEmailAccountsAsync();
    Task<EmailAccount> GetEmailAccountByIdAsync(short id);
}