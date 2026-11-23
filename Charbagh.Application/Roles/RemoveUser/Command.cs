using MediatR;

namespace Charbagh.Application.Roles.RemoveUser;

public sealed record RemoveUserFromRoleCommand(
    Guid RoleId,
    Guid UserBranchId
) : IRequest;