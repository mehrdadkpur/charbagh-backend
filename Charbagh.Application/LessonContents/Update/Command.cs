
using Charbagh.Application.LessonContents.Common;
using MediatR;

namespace Charbagh.Application.LessonContents.Update
{
    public sealed record UpdateLessonContentCommand(
        Guid Id,
        Guid BookItemId,
        string? FromReference,
        string? ToReference,
        string? TeacherNote
        ) : IRequest<LessonContentDto>;
}
