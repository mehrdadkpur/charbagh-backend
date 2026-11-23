using Charbagh.Application.Permissions.Common;
using MediatR;

namespace Charbagh.Application.Permissions.GetByIsSystem
{
    public sealed record GetPermissionsByIsSystemQuery(bool IsSystem) : IRequest<IReadOnlyList<PermissionDto>>;
}
