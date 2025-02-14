using EmailSender.Core.Domain.Entities;

namespace EmailSender.Core.Domain.Repositories;

public interface IEmailAccountRepository
{
    Task<IEnumerable<EmailAccount>> GetEmailAccountsAsync();
    Task<EmailAccount?> GetEmailAccountByIdAsync(short id);
    Task AddAsync(EmailAccount emailAccount);
    void Update(EmailAccount emailAccount);
    void Remove(EmailAccount emailAccount);
}