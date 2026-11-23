
using Charbagh.Application.LessonAssignments.Common;
using MediatR;

namespace Charbagh.Application.LessonAssignments.GetByStudentId
{
    public sealed record GetLessonAssignmentByStudentIdQuery(Guid StudentId): IRequest<LessonAssignmentDto>;
}
