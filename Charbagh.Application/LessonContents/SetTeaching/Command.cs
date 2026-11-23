
using Charbagh.Application.LessonContents.Common;
using MediatR;

namespace Charbagh.Application.LessonContents.SetTeaching
{
    public sealed record SetTeachingLessonContentTypeCommand(Guid Id) : IRequest<LessonContentDto>;

}
