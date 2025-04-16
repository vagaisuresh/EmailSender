using EmailSender.Core.Domain.Entities;
using EmailSender.Core.Domain.Repositories;
using EmailSender.Core.Persistence.Context;

namespace EmailSender.Core.Persistence.Repositories;

public class MessageRecipientRepository : RepositoryBase, IMessageRecipientRepository
{
    public MessageRecipientRepository(AppDbContext context) 
        : base(context)
    {
    }

    public async Task<MessageRecipient?> GetMessageRecipientByIdAsync(int id)
    {
        return await _context.MessageRecipients.FindAsync(id);
    }

    public async Task AddAsync(MessageRecipient messageRecipient)
    {
        await _context.MessageRecipients.AddAsync(messageRecipient);
    }

    // public async Task AddRangeAsync(ICollection<MessageRecipient> messageRecipients)
    // {
    //     await _context.MessageRecipients.AddRangeAsync(messageRecipients);
    // }

    public void Update(MessageRecipient messageRecipient)
    {
        _context.MessageRecipients.Update(messageRecipient);
    }

    public void Remove(MessageRecipient messageRecipient)
    {
        _context.MessageRecipients.Remove(messageRecipient);
    }
}