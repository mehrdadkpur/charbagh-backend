using Charbagh.Application.Roles.Common;
using MediatR;

namespace Charbagh.Application.Roles.GetByIsSystem
{
    public sealed record GetRolesByIsSystemQuery(bool IsSystem) : IRequest<IReadOnlyList<RoleDto>>;
}
