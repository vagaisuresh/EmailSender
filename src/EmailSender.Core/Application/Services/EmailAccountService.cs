using EmailSender.Core.Application.Interfaces;
using EmailSender.Core.Domain.Entities;
using EmailSender.Core.Domain.Repositories;

namespace EmailSender.Core.Application.Services;

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

    public Task<EmailAccount> CreateEmailAccountAsync(EmailAccount account)
    {
        throw new NotImplementedException();
    }

    public Task<EmailAccount> UpdateEmailAccountAsync(short id, EmailAccount account)
    {
        throw new NotImplementedException();
    }

    public Task<EmailAccount> DeleteEmailAccountAsync(short id)
    {
        throw new NotImplementedException();
    }
}