using MediatR;

namespace Charbagh.Application.Roles.GetById
{
    public sealed record GetRoleByIdQuery(Guid Id) : IRequest<RoleDetailsDto>;
}
