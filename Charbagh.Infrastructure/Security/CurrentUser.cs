using System.Security.Claims;
using Charbagh.Application.Abstractions.Security;
using Microsoft.AspNetCore.Http;

namespace Charbagh.Infrastructure.Security;

public sealed class CurrentUser : ICurrentUser
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUser(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public bool IsAuthenticated =>
        _httpContextAccessor.HttpContext?
            .User
            .Identity?
            .IsAuthenticated
        ?? false;

    public Guid UserId
    {
        get
        {
            var value = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (!Guid.TryParse(value, out var userId))
                throw new InvalidOperationException(
                    "Current user id is not available.");

            return userId;
        }
    }

    public string? Mobile =>
        _httpContextAccessor.HttpContext?
            .User
            .FindFirst(ClaimTypes.MobilePhone)?.Value;

    public string? FullName =>
        _httpContextAccessor.HttpContext?
            .User
            .FindFirst(ClaimTypes.Name)?.Value;
}