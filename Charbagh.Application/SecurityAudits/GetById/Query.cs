using Charbagh.Application.SecurityAudits.Common;
using MediatR;

namespace Charbagh.Application.SecurityAudits.GetById;

public sealed record GetSecurityAuditByIdQuery(Guid Id) : IRequest<SecurityAuditDto>;