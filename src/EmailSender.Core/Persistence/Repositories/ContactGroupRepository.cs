using EmailSender.Core.Domain.Entities;
using EmailSender.Core.Domain.Repositories;
using EmailSender.Core.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace EmailSender.Core.Persistence.Repositories;

public class ContactGroupRepository : RepositoryBase, IContactGroupRepository
{
    public ContactGroupRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<ContactGroupMaster>> GetContactGroupsAsync()
    {
        return await _context.ContactGroupMasters
            .Where(m => m.IsRemoved == false)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<ContactGroupMaster?> GetContactGroupAsync(int id)
    {
        return await _context.ContactGroupMasters
            .AsNoTracking()
            .FirstOrDefaultAsync(a => a.Id == id);
    }

    public async Task<ContactGroupMaster?> GetContactGroupByIdAsync(int id)
    {
        return await _context.ContactGroupMasters.FindAsync(id);
    }

    public async Task AddAsync(ContactGroupMaster contactGroupMaster)
    {
        await _context.ContactGroupMasters.AddAsync(contactGroupMaster);
    }

    public void Update(ContactGroupMaster contactGroupMaster)
    {
        _context.ContactGroupMasters.Update(contactGroupMaster);
    }

    public void Remove(ContactGroupMaster contactGroupMaster)
    {
        _context.ContactGroupMasters.Remove(contactGroupMaster);
    }
}