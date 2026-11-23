using Charbagh.Application.Abstractions.Persistence.Repositories.Identity;
using Charbagh.Application.Common.Exceptions;
using Charbagh.Application.Features.Identity.LoginHistories.Dtos;
using Charbagh.Application.Features.Identity.LoginHistories.Mappers;
using MediatR;

namespace Charbagh.Application.LoginHistories.GetById;

public sealed class GetLoginHistoryByIdHandler: IRequestHandler<GetLoginHistoryByIdQuery, LoginHistoryDto>
{
    private readonly ILoginHistoryRepository _loginHistoryRepository;

    public GetLoginHistoryByIdHandler(
        ILoginHistoryRepository loginHistoryRepository)
    {
        _loginHistoryRepository = loginHistoryRepository;
    }

    public async Task<LoginHistoryDto> Handle( GetLoginHistoryByIdQuery request, CancellationToken cancellationToken)
    {
        var loginHistory = await _loginHistoryRepository.GetByIdAsync( request.Id, cancellationToken);

        if (loginHistory is null)
            throw new LoginHistoryNotFoundException(request.Id);

        return loginHistory.ToDto();
    }
}