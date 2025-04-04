using EmailSender.Core.Domain.Entities;
using EmailSender.Core.Domain.Repositories;
using EmailSender.Core.Persistence.Context;

namespace EmailSender.Core.Persistence.Repositories;

public class MessageAttachmentRepository : RepositoryBase, IMessageAttachmentRepository
{
    public MessageAttachmentRepository(AppDbContext context) 
        : base(context)
    {
    }
    
    public async Task<MessageAttachment?> GetAttachmentByIdAsync(int id)
    {
        return await _context.MessageAttachments.FindAsync(id);
    }

    public async Task AddAsync(MessageAttachment messageAttachment)
    {
        await _context.MessageAttachments.AddAsync(messageAttachment);
    }

    public void Update(MessageAttachment messageAttachment)
    {
        _context.Update(messageAttachment);
    }

    public void Remove(MessageAttachment messageAttachment)
    {
        _context.Remove(messageAttachment);
    }
}