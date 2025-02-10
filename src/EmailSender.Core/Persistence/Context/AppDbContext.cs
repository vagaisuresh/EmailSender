using EmailSender.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EmailSender.Core.Persistence.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<EmailAccount> EmailAccounts{ get; set; }
}