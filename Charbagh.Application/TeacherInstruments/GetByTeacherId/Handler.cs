using Charbagh.Application.Abstractions.Persistence.Repositories.People;
using Charbagh.Application.TeacherInstruments.Common;
using MediatR;

namespace Charbagh.Application.TeacherInstruments.GetByTeacherId;

public sealed class GetTeacherInstrumentsByTeacherIdHandler : IRequestHandler<GetTeacherInstrumentsByTeacherIdQuery,IReadOnlyList<TeacherInstrumentDto>>
{
    private readonly ITeacherInstrumentRepository _repository;

    public GetTeacherInstrumentsByTeacherIdHandler( ITeacherInstrumentRepository repository)
    {
        _repository = repository;
    }

    public async Task<IReadOnlyList<TeacherInstrumentDto>> Handle( GetTeacherInstrumentsByTeacherIdQuery request, CancellationToken cancellationToken)
    {
        if (request.TeacherId == Guid.Empty)
            throw new ArgumentException(
                "Teacher id is required.",
                nameof(request.TeacherId));

        var items = await _repository.GetByTeacherIdAsync(
            request.TeacherId,
            cancellationToken);

        return items
            .Select(x => x.ToDto())
            .ToList();
    }
}