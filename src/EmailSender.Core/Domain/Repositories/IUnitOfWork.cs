namespace EmailSender.Core.Domain.Repositories;

public interface IUnitOfWork : IDisposable
{
    IAccountRepository AccountRepository { get; }
    IContactGroupRepository ContactGroupRepository { get; }
    IContactRepository ContactRepository { get; }
    IMessageRepository MessageRepository { get; }
    IMessageAttachmentRepository MessageAttachmentRepository { get; }

    /// <summary>
    /// SaveAsync is used to commit changes to the database. It wraps the call to SaveChangesAsync on the database context.
    /// </summary>
    /// <returns></returns>
    Task SaveAsync();
}