namespace EmailSender.Core.Domain.Repositories;

public interface IUnitOfWork : IDisposable
{
    IEmailAccountRepository EmailAccountRepository { get; }
    IContactGroupRepository ContactGroupRepository { get; }
    IContactRepository ContactRepository { get; }

    /// <summary>
    /// SaveAsync is used to commit changes to the database. It wraps the call to SaveChangesAsync on the database context.
    /// </summary>
    /// <returns></returns>
    Task SaveAsync();
}