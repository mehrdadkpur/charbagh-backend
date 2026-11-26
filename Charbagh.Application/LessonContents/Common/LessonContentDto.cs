
namespace Charbagh.Application.LessonContents.Common
{
    public sealed record LessonContentDto(
        Guid Id,
        Guid LessonSessionId,
        Guid BookItemId,
        LessonContentType Type,
        string? FromReference,
        string? ToReference,
        string? TeacherNote
        );
}
