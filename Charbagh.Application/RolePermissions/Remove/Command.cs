using Charbagh.Application.RolePermissions.Common;
using MediatR;

namespace Charbagh.Application.RolePermissions.Remove
{
    public sealed record RemovePermissionFromRoleCommand(Guid RoleId , Guid PermissionId) : IRequest;
}
