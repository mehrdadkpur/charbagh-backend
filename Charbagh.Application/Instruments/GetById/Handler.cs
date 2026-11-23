using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Instruments.Common;
using Charbagh.Application.Instruments.GetById;
using Charbagh.Application.Common.Exceptions;
using MediatR;

public sealed class GetInstrumentByIdHandler : IRequestHandler<GetInstrumentByIdQuery, InstrumentDto>
{
    private readonly IInstrumentRepository _repository;

    public GetInstrumentByIdHandler(IInstrumentRepository repository)
    {
        _repository = repository;
    }

    public async Task<InstrumentDto> Handle(GetInstrumentByIdQuery request, CancellationToken cancellationToken)
    {
        var instrument = await _repository.GetByIdAsync(request.Id, cancellationToken);

        if (instrument is null)
            throw new BranchNotFoundException(request.Id);

        return instrument.ToDto();
    }
}