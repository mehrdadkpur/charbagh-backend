using Charbagh.Application.SecurityAudits.Common;
using Charbagh.Domain.Enums;
using MediatR;

namespace Charbagh.Application.SecurityAudits.GetByAction;

public sealed record GetSecurityAuditsByActionQuery(SecurityAuditAction Action) : IRequest<IReadOnlyList<SecurityAuditDto>>;