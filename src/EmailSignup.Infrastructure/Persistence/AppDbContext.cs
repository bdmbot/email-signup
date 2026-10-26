using EmailSignup.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EmailSignup.Infrastructure.Persistence;

public sealed class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Subscriber> Subscribers => Set<Subscriber>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var subscriber = modelBuilder.Entity<Subscriber>();

        subscriber.ToTable("subscribers");
        subscriber.HasKey(x => x.Id);
        subscriber.Property(x => x.Email).HasMaxLength(256).IsRequired();
        subscriber.Property(x => x.DisplayName).HasMaxLength(120).IsRequired();
        subscriber.Property(x => x.Region).HasConversion<int>().IsRequired();
        subscriber.Property(x => x.CreatedAtUtc).IsRequired();
        subscriber.HasIndex(x => x.Email).IsUnique();
    }
}
