
namespace Charbagh.Application.PracticeAssignments.Common
{
    public sealed record PracticeAssignmentDto(
        Guid Id,
        Guid LessonSessionId,
        Guid BookItemId,
        string? FromReference,
        string? ToReference,
        string? Instruction,
        DateOnly? DueDate,
        PracticePriority Priority
        );
}
