using EmailSender.Core.Domain.Entities;

namespace EmailSender.Core.Application.Interfaces;

public interface IContactGroupService
{
    Task<IEnumerable<ContactGroupMaster>> GetContactGroupsAsync();
    Task<ContactGroupMaster?> GetContactGroupsByIdAsync(int id);
    Task<ContactGroupMaster> CreateContactGroupAsync(ContactGroupMaster contactGroupMaster);
    Task UpdateContactGroupAsync(int id, ContactGroupMaster contactGroupMaster);
    Task DeleteContactGroupAsync(int id);
}