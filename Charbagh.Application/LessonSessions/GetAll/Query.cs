
using Charbagh.Application.LessonSessions.Common;
using MediatR;

namespace Charbagh.Application.LessonSessions.GetAll
{
    public sealed record GetAllLessonSessionsQuery(): IRequest<IReadOnlyList< LessonSessionDto>>;
}
