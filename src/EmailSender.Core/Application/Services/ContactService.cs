using EmailSender.Core.Application.Interfaces;
using EmailSender.Core.Domain.Entities;
using EmailSender.Core.Domain.Repositories;

namespace EmailSender.Core.Application.Services;

public class ContactService : IContactService
{
    private readonly IUnitOfWork _unitOfWork;

    public ContactService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<ContactMaster>> GetContactsAsync()
    {
        return await _unitOfWork.ContactRepository.GetContactsAsync();
    }

    public async Task<ContactMaster?> GetContactByIdAsync(int id)
    {
        return await _unitOfWork.ContactRepository.GetContactByIdAsync(id);
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
            // Log here
            throw new ApplicationException($"An error occurred while creating the contact: {ex.Message}");
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
            throw new ApplicationException($"An error occurred while updating the contact: {ex.Message}");
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
            throw new ApplicationException($"An error occurred while removing the contact: {ex.Message}");
        }
    }
}