using Charbagh.Application.Features.Identity.LoginHistories.Dtos;
using Charbagh.Domain.Entities.Identity;

namespace Charbagh.Application.Features.Identity.LoginHistories.Mappers;

public static class LoginHistoryMapper
{
    public static LoginHistoryDto ToDto(
        this LoginHistory loginHistory)
    {
        return new LoginHistoryDto(
            loginHistory.Id,
            loginHistory.UserId,
            loginHistory.LoginAt,
            loginHistory.LogoutAt,
            loginHistory.IsSuccessful,
            loginHistory.FailureReason,
            loginHistory.IpAddress,
            loginHistory.UserAgent,
            loginHistory.DeviceName);
    }
}