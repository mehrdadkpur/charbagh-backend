
using Charbagh.Domain.Entities.Education.StudentEducation;

namespace Charbagh.Application.PracticeAssignments.Common
{
    public static class PracticeAssignmentMapper
    {
        public static PracticeAssignmentDto ToDto(this PracticeAssignment practiceAssignment)
        {
            return new PracticeAssignmentDto(
                practiceAssignment.Id,
                practiceAssignment.LessonSessionId,
                practiceAssignment.BookItemId,
                practiceAssignment.FromReference,
                practiceAssignment.ToReference,
                practiceAssignment.Instruction,
                practiceAssignment.DueDate,
                practiceAssignment.Priority
                );
        }

    }

}
