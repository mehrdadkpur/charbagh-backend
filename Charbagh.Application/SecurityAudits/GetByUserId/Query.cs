using Charbagh.Application.SecurityAudits.Common;
using MediatR;

namespace Charbagh.Application.SecurityAudits.GetByUserId;

public sealed record GetSecurityAuditsByUserIdQuery(Guid UserId) : IRequest<IReadOnlyList<SecurityAuditDto>>;