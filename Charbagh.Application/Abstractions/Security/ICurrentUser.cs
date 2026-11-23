namespace Charbagh.Application.Abstractions.Security;

public interface ICurrentUser
{
    Guid UserId { get; }

    string? Mobile { get; }

    string? FullName { get; }

    bool IsAuthenticated { get; }
}