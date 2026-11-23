namespace Charbagh.Application.Features.Identity.LoginHistories.Dtos;

public sealed record LoginHistoryDto(
    Guid Id,
    Guid UserId,
    DateTime LoginAt,
    DateTime? LogoutAt,
    bool IsSuccessful,
    string? FailureReason,
    string? IpAddress,
    string? UserAgent,
    string? DeviceName);