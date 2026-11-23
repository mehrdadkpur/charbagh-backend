
using Charbagh.Application.LessonContents.Common;
using MediatR;

namespace Charbagh.Application.LessonContents.SetPractice
{
    public sealed record SetPracticeLessonContentTypeCommand(Guid Id) : IRequest<LessonContentDto>;

}
