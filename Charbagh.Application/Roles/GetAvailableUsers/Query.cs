using MediatR;

public sealed record GetAvailableUsersForRoleQuery(
    Guid RoleId
) : IRequest<IReadOnlyList<AvailableRoleUserDto>>;