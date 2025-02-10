using EmailSender.Core.Persistence.Context;

namespace EmailSender.Core.Persistence.Repositories;

public abstract class RepositoryBase
{
    protected readonly AppDbContext _context;

    public RepositoryBase(AppDbContext context)
    {
        _context = context;
    }
}