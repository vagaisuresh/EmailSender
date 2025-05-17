using EmailSender.Core.Domain.Entities;

namespace EmailSender.Core.Domain.Repositories;

public interface IContactGroupRepository
{
    Task<IEnumerable<ContactGroupMaster>> GetContactGroupsAsync();
    Task<ContactGroupMaster?> GetContactGroupAsync(int id);
    Task<ContactGroupMaster?> GetContactGroupByIdAsync(int id);

    Task AddAsync(ContactGroupMaster contactGroupMaster);
    void Update(ContactGroupMaster contactGroupMaster);
    void Remove(ContactGroupMaster contactGroupMaster);
}