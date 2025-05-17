using EmailSender.Core.Domain.Entities;

namespace EmailSender.Core.Domain.Repositories;

public interface IContactRepository
{
    Task<IEnumerable<ContactMaster>> GetContactsAsync();
    Task<ContactMaster?> GetContactAsync(int id);
    Task<ContactMaster?> GetContactByIdAsync(int id);

    Task AddAsync(ContactMaster contactMaster);
    void Update(ContactMaster contactMaster);
    void Remove(ContactMaster contactMaster);
}