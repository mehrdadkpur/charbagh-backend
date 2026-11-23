
using Charbagh.Application.Permissions.Common;
using MediatR;

namespace Charbagh.Application.Permissions.GetById
{
    public sealed record GetPermissionByIdQuery(Guid Id) : IRequest<PermissionDto>;
}
