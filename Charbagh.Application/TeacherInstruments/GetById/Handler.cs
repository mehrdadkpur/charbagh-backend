using Charbagh.Application.Abstractions.Persistence.Repositories.People;
using Charbagh.Application.Common.Exceptions;
using Charbagh.Application.TeacherInstruments.Common;
using MediatR;

namespace Charbagh.Application.TeacherInstruments.GetById;

public sealed class GetTeacherInstrumentByIdHandler: IRequestHandler< GetTeacherInstrumentByIdQuery,TeacherInstrumentDto>
{
    private readonly ITeacherInstrumentRepository _repository;

    public GetTeacherInstrumentByIdHandler( ITeacherInstrumentRepository repository)
    {
        _repository = repository;
    }

    public async Task<TeacherInstrumentDto> Handle(
        GetTeacherInstrumentByIdQuery request,
        CancellationToken cancellationToken)
    {
        var teacherInstrument = await _repository.GetByIdAsync(
            request.Id,
            cancellationToken);

        if (teacherInstrument is null)
            throw new TeacherInstrumentNotFoundException(request.Id);

        return teacherInstrument.ToDto();
    }
}