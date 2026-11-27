using Charbagh.Application.Features.Identity.LoginHistories.Dtos;
using MediatR;

namespace Charbagh.Application.LoginHistories.Create;

public sealed record CreateLoginHistoryCommand(
    Guid? UserId,
    string AttemptedMobile,
    bool IsSuccessful,
    string? FailureReason,
    string? IpAddress,
    string? UserAgent,
    string? DeviceName
) : IRequest<LoginHistoryDto>;