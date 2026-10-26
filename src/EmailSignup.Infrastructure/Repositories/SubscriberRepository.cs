using EmailSignup.Application.Interfaces;
using EmailSignup.Domain.Entities;
using EmailSignup.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace EmailSignup.Infrastructure.Repositories;

public sealed class SubscriberRepository : ISubscriberRepository
{
    private readonly AppDbContext _db;

    public SubscriberRepository(AppDbContext db)
    {
        _db = db;
    }

    public Task<bool> ExistsByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        return _db.Subscribers.AnyAsync(x => x.Email == email, cancellationToken);
    }

    public async Task AddAsync(Subscriber subscriber, CancellationToken cancellationToken = default)
    {
        await _db.Subscribers.AddAsync(subscriber, cancellationToken);
    }

    public Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return _db.SaveChangesAsync(cancellationToken);
    }
}
