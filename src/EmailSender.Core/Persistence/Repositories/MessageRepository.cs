using EmailSender.Core.Domain.Entities;
using EmailSender.Core.Domain.Repositories;
using EmailSender.Core.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace EmailSender.Core.Persistence.Repositories;

public class MessageRepository : RepositoryBase, IMessageRepository
{
    public MessageRepository(AppDbContext context) 
        : base(context)
    {
    }

    public async Task<IEnumerable<Message>> GetMessagesAsync()
    {
        return await _context.Messages
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Message?> GetMessageAsync(int id)
    {
        return await _context.Messages
            .Include(a => a.EmailAccountNavigation)
            .Include(a => a.MessageAttachments)
            .Include(a => a.MessageRecipients)
            .ThenInclude(a => a.ContactMasterNavigation)
            .FirstOrDefaultAsync(i => i.Id == id);
    }

    public async Task<Message?> GetMessageByIdAsync(int id)
    {
        return await _context.Messages.FindAsync(id);
    }

    public async Task AddAsync(Message message)
    {
        await _context.Messages.AddAsync(message);
    }

    public void Update(Message message)
    {
        _context.Messages.Update(message);
    }

    public void Remove(Message message)
    {
        _context.Messages.Remove(message);
    }
}