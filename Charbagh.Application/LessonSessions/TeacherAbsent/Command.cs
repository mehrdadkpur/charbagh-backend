
using Charbagh.Application.LessonSessions.Common;
using MediatR;

namespace Charbagh.Application.LessonSessions.TeacherAbsent
{
    public sealed record TeacherAbsentLessonSessionCommand(Guid Id) : IRequest<LessonSessionDto>;
}
