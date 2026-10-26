using EmailSignup.Domain.Enums;

namespace EmailSignup.Application.DTOs;

public sealed class SubscribeRequest
{
    public string Email { get; set; } = string.Empty;
    public string DisplayName { get; set; } = string.Empty;
    public Region Region { get; set; }
}
