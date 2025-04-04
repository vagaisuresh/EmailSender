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

    public Task<IEnumerable<MessageAttachment>> GetAttachmentsByMessageIdAsync(int messageId)
    {
        throw new NotImplementedException();
    }

    public Task<MessageAttachment> GetAttachmentByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task AddAsync(MessageAttachment messageAttachment)
    {
        throw new NotImplementedException();
    }

    public void Update(MessageAttachment messageAttachment)
    {
        throw new NotImplementedException();
    }
    
    public void Remove(MessageAttachment messageAttachment)
    {
        throw new NotImplementedException();
    }
}