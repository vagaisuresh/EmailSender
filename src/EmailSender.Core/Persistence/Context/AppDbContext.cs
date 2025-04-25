using EmailSender.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EmailSender.Core.Persistence.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<EmailAccount> EmailAccounts{ get; set; }
    public DbSet<ContactGroupMaster> ContactGroupMasters { get; set; }
    public DbSet<ContactMaster> ContactMasters { get; set; }

    public DbSet<Message> Messages { get; set; }
    public DbSet<MessageAttachment> MessageAttachments { get; set; }
    public DbSet<MessageRecipient> MessageRecipients { get; set; }

    public DbSet<EmailLog> EmailLogs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Message>()
            .HasMany(m => m.MessageAttachments)
            .WithOne()
            .HasForeignKey(a => a.MessageId)
            .OnDelete(DeleteBehavior.Cascade);
        
        modelBuilder.Entity<Message>()
            .HasMany(m => m.MessageRecipients)
            .WithOne()
            .HasForeignKey(a => a.MessageId)
            .OnDelete(DeleteBehavior.Cascade);
        
        base.OnModelCreating(modelBuilder);
    }
}