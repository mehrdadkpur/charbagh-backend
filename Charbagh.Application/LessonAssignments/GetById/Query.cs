
using Charbagh.Application.LessonAssignments.Common;
using MediatR;

namespace Charbagh.Application.LessonAssignments.GetById
{
    public sealed record GetLessonAssignmentByIdQuery(Guid Id) : IRequest<LessonAssignmentDto>;
}
