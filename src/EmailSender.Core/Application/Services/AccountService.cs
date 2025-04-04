using EmailSender.Core.Application.Interfaces;
using EmailSender.Core.Domain.Entities;
using EmailSender.Core.Domain.Repositories;

namespace EmailSender.Core.Application.Services;

public class AccountService : IAccountService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILoggerService _logger;

    public AccountService(IUnitOfWork unitOfWork, ILoggerService logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<IEnumerable<EmailAccount>> GetEmailAccountsAsync()
    {
        try
        {
            return await _unitOfWork.AccountRepository.GetEmailAccountsAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError($"An error occurred while getting email accounts in GetEmailAccountsAsync service method: {ex}");
            throw new Exception("An error occurred when getting the email account.");
        }
    }

    public async Task<EmailAccount?> GetEmailAccountByIdAsync(short id)
    {
        try
        {
            return await _unitOfWork.AccountRepository.GetEmailAccountByIdAsync(id);
        }
        catch (Exception ex)
        {
            _logger.LogError($"An error occurred while getting email account in GetEmailAccountByIdAsync service method: {ex}");
            throw new Exception("An error occurred when getting the email account.");
        }
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
            _logger.LogError($"An error occurred while creating email account in CreateEmailAccountAsync service method: {ex}");
            throw new Exception("An error occurred when saving the email account.");
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
            _logger.LogError($"An error occurred while updating email account in UpdateEmailAccountAsync (Id: {id}) service method: {ex}");
            throw new Exception("An error occurred when updating the email account.");
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
            _logger.LogError($"An error occurred while deleting email account in DeleteEmailAccountAsync (Id: {id}) service method: {ex}");
            throw new Exception("An error occurred when deleting the email account.");
        }
    }
}