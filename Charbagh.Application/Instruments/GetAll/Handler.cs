using Charbagh.Application.Abstractions.Persistence;

using Charbagh.Application.Instruments.Common;
using Charbagh.Application.Users.GetAll;
using MediatR;

namespace Charbagh.Application.Instruments.GetAll;

public sealed class GetAllInstrumentsHandler : IRequestHandler<GetAllInstrumentsQuery,IReadOnlyList<InstrumentDto>>
{
    private readonly IInstrumentRepository _repository;

    public GetAllInstrumentsHandler(IInstrumentRepository repository)
    {
        _repository = repository;
    }

    public async Task<IReadOnlyList<InstrumentDto>> Handle(GetAllInstrumentsQuery request,CancellationToken cancellationToken)
    {
        var instruments = await _repository.GetAllAsync(cancellationToken);

        return instruments
            .Select(x => x.ToDto())
            .ToList();
    }
}