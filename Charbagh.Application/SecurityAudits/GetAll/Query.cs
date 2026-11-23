using Charbagh.Application.SecurityAudits.Common;
using MediatR;

namespace Charbagh.Application.SecurityAudits.GetAll;

public sealed record GetAllSecurityAuditsQuery: IRequest<IReadOnlyList<SecurityAuditDto>>;