namespace EmailSignup.Application.DTOs;

public sealed class SubscribeResult
{
    public bool Success { get; init; }
    public string Message { get; init; } = string.Empty;

    public static SubscribeResult Ok(string message) =>
        new() { Success = true, Message = message };

    public static SubscribeResult Fail(string message) =>
        new() { Success = false, Message = message };
}
