using EmailSender.Core.Application.Interfaces;
using EmailSender.Core.Domain.Entities;
using EmailSender.Core.Domain.Repositories;

namespace EmailSender.Core.Application.Services;

public class ContactGroupService : IContactGroupService
{
    private readonly IUnitOfWork _unitOfWork;

    public ContactGroupService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    public async Task<IEnumerable<ContactGroupMaster>> GetContactGroupsAsync()
    {
        return await _unitOfWork.ContactGroupRepository.GetContactGroupMastersAsync();
    }

    public async Task<ContactGroupMaster?> GetContactGroupsByIdAsync(int id)
    {
        return await _unitOfWork.ContactGroupRepository.GetContactGroupMasterByIdAsync(id);
    }

    public async Task<ContactGroupMaster> CreateContactGroupAsync(ContactGroupMaster contactGroupMaster)
    {
        try
        {
            await _unitOfWork.ContactGroupRepository.AddAsync(contactGroupMaster);
            await _unitOfWork.SaveAsync();

            return contactGroupMaster;
        }
        catch (Exception ex)
        {
            throw new Exception($"An error occurred when saving the email account: {ex.Message}");
        }
    }

    public async Task UpdateContactGroupAsync(int id, ContactGroupMaster contactGroupMaster)
    {
        var existingGroup = await _unitOfWork.ContactGroupRepository.GetContactGroupMasterByIdAsync(id);

        if (existingGroup == null)
            throw new InvalidOperationException("Requested contact group not found.");
        
        existingGroup.GroupName = contactGroupMaster.GroupName;
        existingGroup.Description = contactGroupMaster.Description;
        existingGroup.IsActive = contactGroupMaster.IsActive;

        try
        {
            _unitOfWork.ContactGroupRepository.Update(existingGroup);
            await _unitOfWork.SaveAsync();
        }
        catch (Exception ex)
        {
            throw new Exception($"An error occurred while updating the contact group: {ex.Message}");
        }
    }

    public async Task DeleteContactGroupAsync(int id)
    {
        var existingGroup = await _unitOfWork.ContactGroupRepository.GetContactGroupMasterByIdAsync(id);

        if (existingGroup == null)
            throw new InvalidOperationException("Requested contact group not found.");
        
        try
        {
            _unitOfWork.ContactGroupRepository.Remove(existingGroup);
            await _unitOfWork.SaveAsync();
        }
        catch (Exception ex)
        {
            throw new Exception($"An error occurred while deleting the contact group: {ex.Message}");
        }
    }
}