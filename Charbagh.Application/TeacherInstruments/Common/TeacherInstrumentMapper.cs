using Charbagh.Domain.Entities.People;

namespace Charbagh.Application.TeacherInstruments.Common;

public static class TeacherInstrumentMapper
{
    public static TeacherInstrumentDto ToDto(
        this TeacherInstrument teacherInstrument)
    {
        return new TeacherInstrumentDto(
            teacherInstrument.Id,
            teacherInstrument.TeacherId,
            teacherInstrument.InstrumentId,
            teacherInstrument.StartTeachingDate,
            teacherInstrument.TeachingLevel,
            teacherInstrument.DefaultSessionFee,
            teacherInstrument.IsActive,
            teacherInstrument.DefaultBookId);
    }
}