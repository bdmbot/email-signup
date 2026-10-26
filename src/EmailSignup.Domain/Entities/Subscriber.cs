using EmailSignup.Domain.Enums;

namespace EmailSignup.Domain.Entities;

public sealed class Subscriber
{
    public Guid Id { get; private set; }
    public string Email { get; private set; } = string.Empty;
    public string DisplayName { get; private set; } = string.Empty;
    public Region Region { get; private set; }
    public DateTimeOffset CreatedAtUtc { get; private set; }

    private Subscriber()
    {
    }

    public static Subscriber Create(string email, string displayName, Region region)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(email);
        ArgumentException.ThrowIfNullOrWhiteSpace(displayName);

        return new Subscriber
        {
            Id = Guid.NewGuid(),
            Email = email.Trim().ToLowerInvariant(),
            DisplayName = displayName.Trim(),
            Region = region,
            CreatedAtUtc = DateTimeOffset.UtcNow
        };
    }
}
