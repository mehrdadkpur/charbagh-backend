using Charbagh.Application.Permissions.Common;
using MediatR;

namespace Charbagh.Application.Permissions.GetByModule
{
    public sealed record GetPermissionsByModuleQuery(string Module) : IRequest<IReadOnlyList<PermissionDto>>;
}
