using EmailSender.Core.Domain.Repositories;
using EmailSender.Core.Persistence.Context;

namespace EmailSender.Core.Persistence.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;

    private IEmailAccountRepository _emailAccountRepository;
    private IContactGroupRepository _contactGroupRepository;

    public UnitOfWork(AppDbContext context, 
        IEmailAccountRepository emailAccountRepository,
        IContactGroupRepository contactGroupRepository)
    {
        _context = context;
        _emailAccountRepository = emailAccountRepository;
        _contactGroupRepository = contactGroupRepository;
    }

    public IEmailAccountRepository EmailAccountRepository => _emailAccountRepository;
    public IContactGroupRepository ContactGroupRepository => _contactGroupRepository;

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