using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Abstractions.Persistence.Repositories.Identity;
using Charbagh.Application.Common.Exceptions;
using Charbagh.Application.Features.Identity.LoginHistories.Dtos;
using Charbagh.Application.Features.Identity.LoginHistories.Mappers;
using MediatR;

namespace Charbagh.Application.LoginHistories.RecordLogout;

public sealed class RecordLogoutHandler: IRequestHandler<RecordLogoutCommand, LoginHistoryDto>
{
    private readonly ILoginHistoryRepository _loginHistoryRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RecordLogoutHandler(ILoginHistoryRepository loginHistoryRepository,IUnitOfWork unitOfWork)
    {
        _loginHistoryRepository = loginHistoryRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<LoginHistoryDto> Handle( RecordLogoutCommand request, CancellationToken cancellationToken)
    {
        var loginHistory = await _loginHistoryRepository.GetForUpdateAsync( request.Id, cancellationToken);

        if (loginHistory is null)
            throw new LoginHistoryNotFoundException(request.Id);

        loginHistory.RecordLogout();

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return loginHistory.ToDto();
    }
}