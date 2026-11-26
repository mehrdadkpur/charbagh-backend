
using Charbagh.Domain.Entities.Education.StudentEducation;

namespace Charbagh.Application.LessonAssignments.Common
{
    public static class LessonAssignmentMapper
    {
        public static LessonAssignmentDto ToDto(this LessonAssignment lessonAssignment)
        {
            return new LessonAssignmentDto(
                lessonAssignment.Id,
                lessonAssignment.StudentId,
                lessonAssignment.BookId,
                lessonAssignment.TeacherId,
                lessonAssignment.InstrumentId,
                lessonAssignment.StartDate,
                lessonAssignment.EndDate,
                lessonAssignment.SessionFee,
                lessonAssignment.PaymentPlan,
                lessonAssignment.Notes,
                lessonAssignment.Status
                );
        }
    }
}
