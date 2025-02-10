using EmailSender.Core.Domain.Entities;
using EmailSender.Core.Domain.Repositories;
using EmailSender.Core.Interfaces;

namespace EmailSender.Core.Services;

public class EmailAccountService : IEmailAccountService
{
    private readonly IEmailAccountRepository _repository;

    public EmailAccountService(IEmailAccountRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<EmailAccount>> GetEmailAccountsAsync()
    {
        return await _repository.GetEmailAccountsAsync();
    }

    public async Task<EmailAccount> GetEmailAccountByIdAsync(short id)
    {
        return await _repository.GetEmailAccountByIdAsync(id);
    }
}