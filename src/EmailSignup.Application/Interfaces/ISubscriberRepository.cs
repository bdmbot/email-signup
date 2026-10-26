using EmailSignup.Domain.Entities;

namespace EmailSignup.Application.Interfaces;

public interface ISubscriberRepository
{
    Task<bool> ExistsByEmailAsync(string email, CancellationToken cancellationToken = default);
    Task AddAsync(Subscriber subscriber, CancellationToken cancellationToken = default);
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}
