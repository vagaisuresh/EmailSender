using EmailSender.Core.Domain.Repositories;
using EmailSender.Core.Persistence.Context;

namespace EmailSender.Core.Persistence.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;

    private IAccountRepository _accountRepository;
    private IContactGroupRepository _contactGroupRepository;
    private IContactRepository _contactRepository;
    private IMessageRepository _messageRepository;
    private IMessageAttachmentRepository _messageAttachmentRepository;

    public UnitOfWork(AppDbContext context, 
        IAccountRepository accountRepository,
        IContactGroupRepository contactGroupRepository, 
        IContactRepository contactRepository,
        IMessageRepository messageRepository,
        IMessageAttachmentRepository messageAttachmentRepository)
    {
        _context = context;
        _accountRepository = accountRepository;
        _contactGroupRepository = contactGroupRepository;
        _contactRepository = contactRepository;
        _messageRepository = messageRepository;
        _messageAttachmentRepository = messageAttachmentRepository;
    }

    public IAccountRepository AccountRepository => _accountRepository;
    public IContactGroupRepository ContactGroupRepository => _contactGroupRepository;
    public IContactRepository ContactRepository => _contactRepository;
    public IMessageRepository MessageRepository => _messageRepository;
    public IMessageAttachmentRepository MessageAttachmentRepository => _messageAttachmentRepository;

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