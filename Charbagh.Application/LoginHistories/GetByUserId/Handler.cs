using Charbagh.Application.Abstractions.Persistence.Repositories.Identity;
using Charbagh.Application.Features.Identity.LoginHistories.Dtos;
using Charbagh.Application.Features.Identity.LoginHistories.Mappers;
using MediatR;

namespace Charbagh.Application.LoginHistories.GetByUserId;

public sealed class GetLoginHistoriesByUserIdHandler : IRequestHandler< GetLoginHistoriesByUserIdQuery,IReadOnlyList<LoginHistoryDto>>
{
    private readonly ILoginHistoryRepository _loginHistoryRepository;

    public GetLoginHistoriesByUserIdHandler( ILoginHistoryRepository loginHistoryRepository)
    {
        _loginHistoryRepository = loginHistoryRepository;
    }

    public async Task<IReadOnlyList<LoginHistoryDto>> Handle( GetLoginHistoriesByUserIdQuery request, CancellationToken cancellationToken)
    {
        if (request.UserId == Guid.Empty)
            throw new ArgumentException( "User id is required.", nameof(request.UserId));

        var histories = await _loginHistoryRepository.GetByUserIdAsync( request.UserId, cancellationToken);

        return histories.Select(x => x.ToDto()).ToList();
    }
}