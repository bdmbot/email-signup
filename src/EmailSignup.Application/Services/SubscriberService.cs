using System.Net.Mail;
using EmailSignup.Application.DTOs;
using EmailSignup.Application.Interfaces;
using EmailSignup.Domain.Entities;
using EmailSignup.Domain.Enums;

namespace EmailSignup.Application.Services;

public sealed class SubscriberService : ISubscriberService
{
    private static readonly IReadOnlyList<RegionOption> Regions =
    [
        new(Region.North, "north", "ภาคเหนือ", "North"),
        new(Region.Northeast, "northeast", "ภาคอีสาน", "Northeast"),
        new(Region.Central, "central", "ภาคกลาง", "Central"),
        new(Region.East, "east", "ภาคตะวันออก", "East"),
        new(Region.West, "west", "ภาคตะวันตก", "West"),
        new(Region.South, "south", "ภาคใต้", "South"),
        new(Region.Overseas, "overseas", "ต่างประเทศ", "Overseas")
    ];

    private readonly ISubscriberRepository _repository;

    public SubscriberService(ISubscriberRepository repository)
    {
        _repository = repository;
    }

    public IReadOnlyList<RegionOption> GetRegions() => Regions;

    public async Task<SubscribeResult> SubscribeAsync(
        SubscribeRequest request,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(request.DisplayName))
        {
            return SubscribeResult.Fail("กรุณากรอกชื่อ");
        }

        if (string.IsNullOrWhiteSpace(request.Email) || !IsValidEmail(request.Email))
        {
            return SubscribeResult.Fail("อีเมลไม่ถูกต้อง");
        }

        if (!Enum.IsDefined(request.Region))
        {
            return SubscribeResult.Fail("กรุณาเลือกภูมิภาค");
        }

        var email = request.Email.Trim().ToLowerInvariant();

        if (await _repository.ExistsByEmailAsync(email, cancellationToken))
        {
            return SubscribeResult.Fail("อีเมลนี้สมัครไว้แล้ว");
        }

        var subscriber = Subscriber.Create(email, request.DisplayName, request.Region);
        await _repository.AddAsync(subscriber, cancellationToken);
        await _repository.SaveChangesAsync(cancellationToken);

        var regionName = Regions.First(r => r.Value == request.Region).NameTh;
        return SubscribeResult.Ok($"สมัครสำเร็จ — ยินดีต้อนรับสู่รายชื่อ {regionName}");
    }

    private static bool IsValidEmail(string email)
    {
        try
        {
            var address = new MailAddress(email);
            return address.Address.Equals(email.Trim(), StringComparison.OrdinalIgnoreCase);
        }
        catch
        {
            return false;
        }
    }
}
