using Charbagh.Application.Abstractions.Persistence;

using Charbagh.Application.Instruments.Common;

using MediatR;

namespace Charbagh.Application.Instruments.GetAll;

public sealed class GetAllInstrumentsHandler : IRequestHandler<GetAllUserssQuery,IReadOnlyList<InstrumentDto>>
{
    private readonly IInstrumentRepository _repository;

    public GetAllInstrumentsHandler(IInstrumentRepository repository)
    {
        _repository = repository;
    }

    public async Task<IReadOnlyList<InstrumentDto>> Handle(GetAllUserssQuery request,CancellationToken cancellationToken)
    {
        var instruments = await _repository.GetAllAsync(cancellationToken);

        return instruments
            .Select(x => x.ToDto())
            .ToList();
    }
}