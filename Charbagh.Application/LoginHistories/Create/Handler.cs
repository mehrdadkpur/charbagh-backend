using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Abstractions.Persistence.Repositories.Identity;
using Charbagh.Application.Features.Identity.LoginHistories.Dtos;
using Charbagh.Application.Features.Identity.LoginHistories.Mappers;
using Charbagh.Domain.Entities.Identity;
using MediatR;

namespace Charbagh.Application.LoginHistories.Create;

public sealed class CreateLoginHistoryHandler: IRequestHandler<CreateLoginHistoryCommand, LoginHistoryDto>
{
    private readonly ILoginHistoryRepository _loginHistoryRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateLoginHistoryHandler( ILoginHistoryRepository loginHistoryRepository, IUnitOfWork unitOfWork)
    {
        _loginHistoryRepository = loginHistoryRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<LoginHistoryDto> Handle( CreateLoginHistoryCommand request,CancellationToken cancellationToken)
    {
        var loginHistory = LoginHistory.Create(
            request.UserId,
            request.AttemptedMobile,
            request.IsSuccessful,
            request.FailureReason,
            request.IpAddress,
            request.UserAgent,
            request.DeviceName);

        await _loginHistoryRepository.AddAsync( loginHistory, cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return loginHistory.ToDto();
    }
}