using EmailSender.Core.Domain.Entities;
using EmailSender.Core.Domain.Repositories;
using EmailSender.Core.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace EmailSender.Core.Persistence.Repositories;

public class AccountRepository : RepositoryBase, IAccountRepository
{
    public AccountRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<EmailAccount>> GetEmailAccountsAsync()
    {
        return await _context.EmailAccounts
            .Where(a => a.IsRemoved == false)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<EmailAccount?> GetEmailAccountAsync(short id)
    {
        return await _context.EmailAccounts
            .AsNoTracking()
            .FirstOrDefaultAsync(a => a.Id == id);
    }

    public async Task<EmailAccount?> GetEmailAccountByIdAsync(short id)
    {
        return await _context.EmailAccounts.FindAsync(id);
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