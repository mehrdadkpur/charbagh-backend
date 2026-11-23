

using Charbagh.Application.LessonSessions.Common;
using MediatR;

namespace Charbagh.Application.LessonSessions.Complete
{
    public sealed record SetTeachingLessonSessionCommand(Guid Id) : IRequest<LessonSessionDto>;
}
