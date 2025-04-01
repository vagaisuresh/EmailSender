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

    public async Task<IEnumerable<Message>> GetAllMessagesAsync()
    {
        return await _context.Messages.ToListAsync();
    }

    public async Task<Message?> GetMessageByIdAsync(int id)
    {
        return await _context.Messages.FindAsync(id);
    }

    public async Task<Message?> GetMessageByIdWithDetailsAsync(int id)
    {
        return await _context.Messages
            .Include(a => a.EmailAccountNavigation)
            .Include(a => a.MessageAttachments)
            .Include(a => a.MessageRecipients)
            .FirstOrDefaultAsync(i => i.Id == id);
    }
}