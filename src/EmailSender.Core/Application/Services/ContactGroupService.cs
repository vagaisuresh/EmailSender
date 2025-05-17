using EmailSender.Core.Application.Interfaces;
using EmailSender.Core.Domain.Entities;
using EmailSender.Core.Domain.Repositories;

namespace EmailSender.Core.Application.Services;

public class ContactGroupService : IContactGroupService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILoggerService _logger;

    public ContactGroupService(IUnitOfWork unitOfWork, ILoggerService logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }
    
    public async Task<IEnumerable<ContactGroupMaster>> GetContactGroupsAsync()
    {
        try
        {
            return await _unitOfWork.ContactGroupRepository.GetContactGroupsAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError($"An error occurred while getting contact groups in GetContactGroupsAsync service method: {ex}");
            throw new Exception("An error occurred while getting the contact groups.");
        }
    }

    public async Task<ContactGroupMaster?> GetContactGroupAsync(int id)
    {
        try
        {
            return await _unitOfWork.ContactGroupRepository.GetContactGroupAsync(id);
        }
        catch (Exception ex)
        {
            _logger.LogError($"An error occurred while getting contact group in GetContactGroupsByIdAsync service method: {ex}");
            throw new Exception("An error occurred while getting the contact group.");
        }
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
            _logger.LogError($"An error occurred while creating contact group in CreateContactGroupAsync service method: {ex}");
            throw new Exception($"An error occurred when saving the email account.");
        }
    }

    public async Task UpdateContactGroupAsync(int id, ContactGroupMaster contactGroupMaster)
    {
        var existingGroup = await _unitOfWork.ContactGroupRepository.GetContactGroupByIdAsync(id);

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
            _logger.LogError($"An error occurred while updating contact group in UpdateContactGroupAsync service method: {ex}");
            throw new Exception($"An error occurred while updating the contact group.");
        }
    }

    public async Task DeleteContactGroupAsync(int id)
    {
        var existingGroup = await _unitOfWork.ContactGroupRepository.GetContactGroupByIdAsync(id);

        if (existingGroup == null)
            throw new InvalidOperationException("Requested contact group not found.");
        
        try
        {
            _unitOfWork.ContactGroupRepository.Remove(existingGroup);
            await _unitOfWork.SaveAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError($"An error occurred while deleting contact group in DeleteContactGroupAsync service method: {ex}");
            throw new Exception($"An error occurred while deleting the contact group.");
        }
    }
}