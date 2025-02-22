using EmailSender.Core.Domain.Repositories;
using EmailSender.Core.Persistence.Context;

namespace EmailSender.Core.Persistence.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;

    private IEmailAccountRepository _emailAccountRepository;

    public UnitOfWork(AppDbContext context, 
        IEmailAccountRepository emailAccountRepository)
    {
        _context = context;
        _emailAccountRepository = emailAccountRepository;
    }

    public IEmailAccountRepository EmailAccountRepository => _emailAccountRepository;

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Dispose of the resources
    /// </summary>
    public void Dispose()
    {
        _context.Dispose();
    }
}