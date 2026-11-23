
namespace Charbagh.Application.Attendances.Common
{
    public sealed record AttendanceDto(
        Guid Id,
        Guid LessonSessionId,
        AttendanceStatus Status,
        DateTime? CheckInAt,
        DateTime? CheckOutAt,
        string? Reason,
        string? Note,
        Guid? RecordedByUserId
        );
}
