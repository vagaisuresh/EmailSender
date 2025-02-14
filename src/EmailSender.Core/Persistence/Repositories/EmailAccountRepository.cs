using EmailSender.Core.Domain.Entities;
using EmailSender.Core.Domain.Repositories;
using EmailSender.Core.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace EmailSender.Core.Persistence.Repositories;

public class EmailAccountRepository : RepositoryBase, IEmailAccountRepository
{
    public EmailAccountRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<EmailAccount>> GetEmailAccountsAsync()
    {
        return await _context.EmailAccounts
            .Where(a => a.IsRemoved == false)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<EmailAccount?> GetEmailAccountByIdAsync(short id)
    {
        //return await _context.EmailAccounts.FindAsync(id) ?? new EmailAccount();

        var emailAccount = await _context.EmailAccounts.FindAsync(id);
        return emailAccount; // ?? throw new KeyNotFoundException($"EmailAccount with ID {id} not found.");
    }

    public async Task AddAsync(EmailAccount emailAccount)
    {
        await _context.EmailAccounts.AddAsync(emailAccount);
    }

    public void Update(EmailAccount emailAccount)
    {
        _context.EmailAccounts.Update(emailAccount);
    }

    public void Remove(EmailAccount emailAccount)
    {
        _context.EmailAccounts.Remove(emailAccount);
    }
}