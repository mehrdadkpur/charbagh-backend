using Charbagh.Application.Abstractions.Persistence.Repositories.People;
using Charbagh.Application.TeacherInstruments.Common;
using MediatR;

namespace Charbagh.Application.TeacherInstruments.GetByInstrumentId;

public sealed class GetTeacherInstrumentsByInstrumentIdHandler : IRequestHandler<GetTeacherInstrumentsByInstrumentIdQuery, IReadOnlyList<TeacherInstrumentDto>>
{
    private readonly ITeacherInstrumentRepository _repository;

    public GetTeacherInstrumentsByInstrumentIdHandler( ITeacherInstrumentRepository repository)
    {
        _repository = repository;
    }

    public async Task<IReadOnlyList<TeacherInstrumentDto>> Handle(GetTeacherInstrumentsByInstrumentIdQuery request, CancellationToken cancellationToken)
    {
        if (request.InstrumentId == Guid.Empty)
            throw new ArgumentException(
                "Instrument id is required.",
                nameof(request.InstrumentId));

        var items = await _repository.GetByInstrumentIdAsync(
            request.InstrumentId,
            cancellationToken);

        return items
            .Select(x => x.ToDto())
            .ToList();
    }
}