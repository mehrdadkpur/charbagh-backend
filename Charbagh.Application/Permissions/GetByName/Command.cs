using Charbagh.Application.Permissions.Common;
using MediatR;

namespace Charbagh.Application.Permissions.GetByName
{
    public sealed record GetPermissionsByNameQuery(string Name) : IRequest<IReadOnlyList<PermissionDto>>;
}
