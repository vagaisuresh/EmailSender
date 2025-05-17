using EmailSender.Core.Domain.Entities;
using EmailSender.Core.Domain.Repositories;
using EmailSender.Core.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace EmailSender.Core.Persistence.Repositories;

public class ContactRepository : RepositoryBase, IContactRepository
{
    public ContactRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<ContactMaster>> GetContactsAsync()
    {
        return await _context.ContactMasters
            .Where(x => x.IsRemoved == false)
            .Include(x => x.ContactGroupMaster)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<ContactMaster?> GetContactAsync(int id)
    {
        return await _context.ContactMasters
            .Include(x => x.ContactGroupMaster)
            .AsNoTracking()
            .FirstOrDefaultAsync(a => a.Id == id);
    }

    public async Task<ContactMaster?> GetContactByIdAsync(int id)
    {
        return await _context.ContactMasters.FindAsync(id);
    }

    public async Task AddAsync(ContactMaster contactMaster)
    {
        await _context.ContactMasters.AddAsync(contactMaster);
    }

    public void Update(ContactMaster contactMaster)
    {
        _context.ContactMasters.Update(contactMaster);
    }

    public void Remove(ContactMaster contactMaster)
    {
        _context.ContactMasters.Remove(contactMaster);
    }
}