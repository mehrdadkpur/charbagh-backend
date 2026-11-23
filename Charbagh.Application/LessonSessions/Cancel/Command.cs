
using Charbagh.Application.LessonSessions.Common;
using MediatR;

namespace Charbagh.Application.LessonSessions.Cancel
{
    public sealed record CancelLessonSessionCommand(Guid Id): IRequest<LessonSessionDto>;
}
