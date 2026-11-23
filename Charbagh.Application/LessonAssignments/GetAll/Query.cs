
using Charbagh.Application.LessonAssignments.Common;
using MediatR;

namespace Charbagh.Application.LessonAssignments.GetAll
{
    public sealed record GetAllLessonAssignmentsQuery():IRequest<IReadOnlyList< LessonAssignmentDto>>;
}
