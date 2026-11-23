using Charbagh.Application.Roles.Common;
using MediatR;

namespace Charbagh.Application.Roles.GetByName
{
    public sealed record GetRolesByNameQuery(string Name) : IRequest<IReadOnlyList<RoleDto>>;
}
