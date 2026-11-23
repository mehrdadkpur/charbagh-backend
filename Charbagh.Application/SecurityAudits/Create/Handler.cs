using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Abstractions.Persistence.Repositories.Identity;
using Charbagh.Application.SecurityAudits.Common;
using Charbagh.Domain.Entities.Identity;
using MediatR;

namespace Charbagh.Application.SecurityAudits.Create;

public sealed class CreateSecurityAuditHandler
    : IRequestHandler<CreateSecurityAuditCommand, SecurityAuditDto>
{
    private readonly ISecurityAuditRepository _securityAuditRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateSecurityAuditHandler(
        ISecurityAuditRepository securityAuditRepository,
        IUnitOfWork unitOfWork)
    {
        _securityAuditRepository = securityAuditRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<SecurityAuditDto> Handle(
        CreateSecurityAuditCommand request,
        CancellationToken cancellationToken)
    {
        var securityAudit = SecurityAudit.Create(
            request.UserId,
            request.Action,
            request.Success,
            request.Description,
            request.IpAddress,
            request.UserAgent,
            request.DeviceName,
            request.Metadata);

        await _securityAuditRepository.AddAsync(
            securityAudit,
            cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return securityAudit.ToDto();
    }
}