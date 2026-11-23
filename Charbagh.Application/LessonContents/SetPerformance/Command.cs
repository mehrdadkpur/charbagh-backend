
using Charbagh.Application.LessonContents.Common;
using MediatR;

namespace Charbagh.Application.LessonContents.SetPerformance
{
    public sealed record SetPerformanceLessonContentTypeCommand (Guid Id) : IRequest<LessonContentDto>;
}
