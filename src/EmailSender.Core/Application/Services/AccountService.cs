using EmailSender.Core.Application.Interfaces;
using EmailSender.Core.Domain.Entities;
using EmailSender.Core.Domain.Repositories;

namespace EmailSender.Core.Application.Services;

public class AccountService : IAccountService
{
    private readonly IUnitOfWork _unitOfWork;

    public AccountService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<EmailAccount>> GetEmailAccountsAsync()
    {
        return await _unitOfWork.AccountRepository.GetEmailAccountsAsync();
    }

    public async Task<EmailAccount?> GetEmailAccountByIdAsync(short id)
    {
        return await _unitOfWork.AccountRepository.GetEmailAccountByIdAsync(id);
    }

    public async Task<EmailAccount> CreateEmailAccountAsync(EmailAccount account)
    {
        try
        {
            await _unitOfWork.AccountRepository.AddAsync(account);
            await _unitOfWork.SaveAsync();
            
            return account;
        }
        catch (Exception ex)
        {
            throw new Exception($"An error occurred when saving the email account: {ex.Message}");
        }
    }

    public async Task UpdateEmailAccountAsync(short id, EmailAccount account)
    {
        var existingEmailAccount = await _unitOfWork.AccountRepository.GetEmailAccountByIdAsync(id);

        if (existingEmailAccount == null)
            throw new InvalidOperationException("Email account not found.");
        
        existingEmailAccount.Name = account.Name;
        existingEmailAccount.EmailAddress = account.EmailAddress;
        existingEmailAccount.OutgoingServer = account.OutgoingServer;
        existingEmailAccount.OutgoingPortNumber = account.OutgoingPortNumber;
        existingEmailAccount.RequiresAuthentication = account.RequiresAuthentication;
        existingEmailAccount.EncryptedConnection = account.EncryptedConnection;
        existingEmailAccount.UserName = account.UserName;
        existingEmailAccount.Password = account.Password;
        existingEmailAccount.IsActive = account.IsActive;

        try
        {
            _unitOfWork.AccountRepository.Update(existingEmailAccount);
            await _unitOfWork.SaveAsync();
        }
        catch (Exception ex)
        {
            throw new Exception($"An error occurred when updating the email account: {ex.Message}");
        }
    }

    public async Task DeleteEmailAccountAsync(short id)
    {
        var existingEmailAccount = await _unitOfWork.AccountRepository.GetEmailAccountByIdAsync(id);

        if (existingEmailAccount == null)
            throw new InvalidOperationException("Email account not found.");

        try
        {
            _unitOfWork.AccountRepository.Remove(existingEmailAccount);
            await _unitOfWork.SaveAsync();
        }
        catch (Exception ex)
        {
            throw new Exception($"An error occurred when deleting the email account: {ex.Message}");
        }
    }
}