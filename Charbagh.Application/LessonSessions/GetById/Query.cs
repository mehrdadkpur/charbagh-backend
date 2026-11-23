
using Charbagh.Application.LessonSessions.Common;
using MediatR;

namespace Charbagh.Application.LessonSessions.GetById
{
    public sealed record GetLessonSessionByIdQuery(Guid Id) : IRequest<LessonSessionDto>;
}
