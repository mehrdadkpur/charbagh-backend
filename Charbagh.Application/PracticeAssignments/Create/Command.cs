using Charbagh.Application.PracticeAssignments.Common;
using MediatR;

namespace Charbagh.Application.PracticeAssignments.Create
{
    public sealed record CreatePracticeAssignmentCommand(
         Guid LessonSessionId,
         Guid BookItemId,
         string? FromReference,
         string? ToReference,
         string? Instruction,
         DateOnly? DueDate,
         PracticePriority Priority
        ) :IRequest<PracticeAssignmentDto>;

}
