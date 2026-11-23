using Charbagh.Application.Roles.Common;
using MediatR;

namespace Charbagh.Application.Roles.GetAll
{
    public sealed record GetAllRolesQuery():IRequest<IReadOnlyList< RoleDto>>;
}
