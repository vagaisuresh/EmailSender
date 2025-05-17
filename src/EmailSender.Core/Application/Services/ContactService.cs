using EmailSender.Core.Application.Interfaces;
using EmailSender.Core.Domain.Entities;
using EmailSender.Core.Domain.Repositories;

namespace EmailSender.Core.Application.Services;

public class ContactService : IContactService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILoggerService _logger;

    public ContactService(IUnitOfWork unitOfWork, ILoggerService logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<IEnumerable<ContactMaster>> GetContactsAsync()
    {
        try
        {
            return await _unitOfWork.ContactRepository.GetContactsAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError($"An error occurred while getting contacts in GetContactsAsync service method: {ex}");
            throw new Exception("An error occurred while getting contact.");
        }
    }

    public async Task<ContactMaster?> GetContactAsync(int id)
    {
        try
        {
            return await _unitOfWork.ContactRepository.GetContactAsync(id);
        }
        catch (Exception ex)
        {
            _logger.LogError($"An error occurred while getting the contact in GetContactByIdAsync service method: {ex}");
            throw new Exception("An error occurred while getting contact.");
        }
    }

    public async Task<ContactMaster> SaveContactAsync(ContactMaster contactMaster)
    {
        try
        {
            await _unitOfWork.ContactRepository.AddAsync(contactMaster);
            await _unitOfWork.SaveAsync();

            return contactMaster;
        }
        catch (Exception ex)
        {
            _logger.LogError($"An error occurred while creating contact in SaveContactAsync service method: {ex}");
            throw new ApplicationException($"An error occurred while creating the contact.");
        }
    }

    public async Task UpdateContactAsync(int id, ContactMaster contactMaster)
    {
        var existingContact = await _unitOfWork.ContactRepository.GetContactByIdAsync(id);

        if (existingContact == null)
            throw new ApplicationException("Contact not found");

        existingContact.GroupId = contactMaster.GroupId;
        existingContact.Salutation = contactMaster.Salutation;
        existingContact.FullName = contactMaster.FullName;
        existingContact.JobTitle = contactMaster.JobTitle;
        existingContact.CompanyName = contactMaster.CompanyName;
        existingContact.Address = contactMaster.Address;
        existingContact.MobileIsd = contactMaster.MobileIsd;
        existingContact.MobileNumber = contactMaster.MobileNumber;
        existingContact.EmailAddress = contactMaster.EmailAddress;
        existingContact.IsActive = contactMaster.IsActive;

        try
        {
            _unitOfWork.ContactRepository.Update(existingContact);
            await _unitOfWork.SaveAsync();
        }
        catch (ApplicationException ex)
        {
            _logger.LogError($"An error occurred while updating contact in UpdateContactAsync service method: {ex}");
            throw new ApplicationException($"An error occurred while updating the contact.");
        }
    }

    public async Task DeleteContactAsync(int id)
    {
        var existingContact = await _unitOfWork.ContactRepository.GetContactByIdAsync(id);

        if (existingContact == null)
            throw new ApplicationException("Contact not found");

        try
        {
            _unitOfWork.ContactRepository.Remove(existingContact);
            await _unitOfWork.SaveAsync();
        }
        catch (ApplicationException ex)
        {
            _logger.LogError($"An error occurred while deleting contact in DeleteContactAsync service method: {ex}");
            throw new ApplicationException($"An error occurred while removing the contact.");
        }
    }
}