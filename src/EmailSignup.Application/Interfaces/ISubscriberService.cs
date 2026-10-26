using EmailSignup.Application.DTOs;

namespace EmailSignup.Application.Interfaces;

public interface ISubscriberService
{
    IReadOnlyList<RegionOption> GetRegions();
    Task<SubscribeResult> SubscribeAsync(SubscribeRequest request, CancellationToken cancellationToken = default);
}
