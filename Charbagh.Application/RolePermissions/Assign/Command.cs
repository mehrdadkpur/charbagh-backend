using Charbagh.Application.RolePermissions.Common;
using MediatR;

namespace Charbagh.Application.RolePermissions.AssignToRole;

public sealed record AssignPermissionToRoleCommand( Guid RoleId, Guid PermissionId) : IRequest<RolePermissionDto>;