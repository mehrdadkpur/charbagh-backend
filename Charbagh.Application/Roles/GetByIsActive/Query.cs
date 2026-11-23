using Charbagh.Application.Roles.Common;
using MediatR;

namespace Charbagh.Application.Roles.GetByIsActive;

public sealed record GetRolesByIsActiveQuery(bool IsActive) : IRequest<IReadOnlyList<RoleDto>>;
