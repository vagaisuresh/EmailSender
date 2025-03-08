using EmailSender.Core.Domain.Repositories;
using EmailSender.Core.Persistence.Context;

namespace EmailSender.Core.Persistence.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;

    private IEmailAccountRepository _emailAccountRepository;
    private IContactGroupRepository _contactGroupRepository;
    private IContactRepository _contactRepository;

    public UnitOfWork(AppDbContext context, 
        IEmailAccountRepository emailAccountRepository,
        IContactGroupRepository contactGroupRepository, 
        IContactRepository contactRepository)
    {
        _context = context;
        _emailAccountRepository = emailAccountRepository;
        _contactGroupRepository = contactGroupRepository;
        _contactRepository = contactRepository;
    }

    public IEmailAccountRepository EmailAccountRepository => _emailAccountRepository;
    public IContactGroupRepository ContactGroupRepository => _contactGroupRepository;
    public IContactRepository ContactRepository => _contactRepository;

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