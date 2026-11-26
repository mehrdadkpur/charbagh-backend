
using Charbagh.Application.Attendances.Common;
using MediatR;

namespace Charbagh.Application.Attendances.Create
{
    public sealed record CreateAttendanceCommand(
        Guid LessonSessionId,
        AttendanceStatus Status,
        DateTime? CheckInAt,
        DateTime? CheckOutAt,
        string? Reason,
        string? Note,
        Guid? RecordedByUserId
        ) :IRequest<AttendanceDto>;
}
