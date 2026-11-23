
using Charbagh.Domain.Entities.Education.StudentEducation;

namespace Charbagh.Application.Attendances.Common
{
    public static class AttendanceMapper
    {
        public static AttendanceDto ToDto(this Attendance attendance)
        {
            return new AttendanceDto(
                attendance.Id,
                attendance.LessonSessionId,
                attendance.Status,
                attendance.CheckInAt,
                attendance.CheckOutAt,
                attendance.Reason,
                attendance.Note,
                attendance.RecordedByUserId
                );
        }
    }
}
