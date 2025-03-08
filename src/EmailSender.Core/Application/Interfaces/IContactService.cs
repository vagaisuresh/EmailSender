using EmailSender.Core.Domain.Entities;

namespace EmailSender.Core.Application.Interfaces;

public interface IContactService
{
    Task<IEnumerable<ContactMaster>> GetContactsAsync();
    Task<ContactMaster?> GetContactByIdAsync(int id);
    Task<ContactMaster> SaveContactAsync(ContactMaster contactMaster);
    Task UpdateContactAsync(int id, ContactMaster contactMaster);
    Task DeleteContactAsync(int id);
}