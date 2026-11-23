using MediatR;

namespace Charbagh.Application.Roles.AssignUser;

public sealed record AssignUserToRoleCommand(
    Guid RoleId,
    Guid UserBranchId
) : IRequest;