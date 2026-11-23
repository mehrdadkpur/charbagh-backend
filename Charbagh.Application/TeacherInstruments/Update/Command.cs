using Charbagh.Application.TeacherInstruments.Common;
using MediatR;

namespace Charbagh.Application.TeacherInstruments.Update;

public sealed record UpdateTeacherInstrumentCommand(
    Guid Id,
    DateOnly? StartTeachingDate,
    TeachingLevel TeachingLevel,
    decimal? DefaultSessionFee,
    Guid? DefaultBookId
) : IRequest<TeacherInstrumentDto>;