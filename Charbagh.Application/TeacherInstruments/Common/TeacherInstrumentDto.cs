
namespace Charbagh.Application.TeacherInstruments.Common;

public sealed record TeacherInstrumentDto(
    Guid Id,
    Guid TeacherId,
    Guid InstrumentId,
    DateOnly? StartTeachingDate,
    TeachingLevel TeachingLevel,
    decimal? DefaultSessionFee,
    bool IsActive,
    Guid? DefaultBookId);