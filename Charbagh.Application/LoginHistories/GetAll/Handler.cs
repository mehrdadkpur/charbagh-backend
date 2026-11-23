using Charbagh.Application.Abstractions.Persistence.Repositories.Identity;
using Charbagh.Application.Features.Identity.LoginHistories.Dtos;
using Charbagh.Application.Features.Identity.LoginHistories.Mappers;
using MediatR;

namespace Charbagh.Application.LoginHistories.GetAll;

public sealed class GetAllLoginHistoriesHandler : IRequestHandler< GetAllLoginHistoriesQuery,IReadOnlyList<LoginHistoryDto>>
{
    private readonly ILoginHistoryRepository _loginHistoryRepository;

    public GetAllLoginHistoriesHandler(ILoginHistoryRepository loginHistoryRepository)
    {
        _loginHistoryRepository = loginHistoryRepository;
    }

    public async Task<IReadOnlyList<LoginHistoryDto>> Handle( GetAllLoginHistoriesQuery request, CancellationToken cancellationToken)
    {
        var histories = await _loginHistoryRepository.GetAllAsync( cancellationToken);

        return histories.Select(x => x.ToDto()).ToList();
    }
}