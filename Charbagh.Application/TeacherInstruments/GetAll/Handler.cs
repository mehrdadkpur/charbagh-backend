using Charbagh.Application.Abstractions.Persistence.Repositories.People;
using Charbagh.Application.TeacherInstruments.Common;
using Charbagh.Application.TeacherInstruments.GetAll;
using MediatR;

namespace Charbagh.Application.Features.People.TeacherInstruments.UseCases.GetAllTeacherInstruments;

public sealed class GetAllTeacherInstrumentsHandler : IRequestHandler< GetAllTeacherInstrumentsQuery,IReadOnlyList<TeacherInstrumentDto>>
{
    private readonly ITeacherInstrumentRepository _repository;

    public GetAllTeacherInstrumentsHandler(ITeacherInstrumentRepository repository)
    {
        _repository = repository;
    }

    public async Task<IReadOnlyList<TeacherInstrumentDto>> Handle( GetAllTeacherInstrumentsQuery request,CancellationToken cancellationToken)
    {
        var items = await _repository.GetAllAsync(cancellationToken);

        return items.Select(x => x.ToDto()).ToList();
    }
}