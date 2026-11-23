
using Charbagh.Application.LessonSessions.Common;
using MediatR;

namespace Charbagh.Application.LessonSessions.StudentAbsent
{
    public sealed record StudentAbsentLessonSessionCommand(Guid Id) : IRequest<LessonSessionDto>;
}
