using EmailSender.Core.Domain.Entities;

namespace EmailSender.Core.Domain.Repositories;

public interface IContactGroupRepository
{
    Task<IEnumerable<ContactGroupMaster>> GetContactGroupMastersAsync();
    Task<ContactGroupMaster?> GetContactGroupMasterByIdAsync(int id);
    Task AddAsync(ContactGroupMaster contactGroupMaster);
    void Update(ContactGroupMaster contactGroupMaster);
    void Remove(ContactGroupMaster contactGroupMaster);
}