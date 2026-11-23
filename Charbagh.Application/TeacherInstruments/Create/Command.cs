using Charbagh.Application.TeacherInstruments.Common;
using MediatR;

namespace Charbagh.Application.TeacherInstruments.Create;

public sealed record CreateTeacherInstrumentCommand(
    Guid TeacherId,
    Guid InstrumentId,
    DateOnly? StartTeachingDate,
    TeachingLevel TeachingLevel,
    decimal? DefaultSessionFee,
    Guid? DefaultBookId
) : IRequest<TeacherInstrumentDto>;