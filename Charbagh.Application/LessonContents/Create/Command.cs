using Charbagh.Application.LessonContents.Common;
using MediatR;

namespace Charbagh.Application.LessonContents.Create
{
    public sealed record CreateLessonContentCommand(
        Guid LessonSessionId,
        Guid BookItemId,
        string? FromReference,
        string? ToReference,
        string? TeacherNote
        ) : IRequest<LessonContentDto>;
}
