using Charbagh.Application.Permissions.Common;
using MediatR;

namespace Charbagh.Application.Permissions.GetAll
{
    public sealed record GetAllPermissionsQuery() : IRequest<IReadOnlyList< PermissionDto>>;
}
