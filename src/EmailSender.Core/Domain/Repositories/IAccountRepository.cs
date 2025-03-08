using EmailSender.Core.Domain.Entities;

namespace EmailSender.Core.Domain.Repositories;

public interface IAccountRepository
{
    Task<IEnumerable<EmailAccount>> GetEmailAccountsAsync();
    Task<EmailAccount?> GetEmailAccountByIdAsync(short id);
    Task AddAsync(EmailAccount emailAccount);
    void Update(EmailAccount emailAccount);
    void Remove(EmailAccount emailAccount);
}