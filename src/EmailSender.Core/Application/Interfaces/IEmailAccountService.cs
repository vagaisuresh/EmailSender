using EmailSender.Core.Domain.Entities;

namespace EmailSender.Core.Application.Interfaces;

public interface IEmailAccountService
{
    Task<IEnumerable<EmailAccount>> GetEmailAccountsAsync();
    Task<EmailAccount> GetEmailAccountByIdAsync(short id);
}