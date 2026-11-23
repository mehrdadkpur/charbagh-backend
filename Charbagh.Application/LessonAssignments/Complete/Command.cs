using Charbagh.Application.LessonAssignments.Common;
using MediatR;

namespace Charbagh.Application.LessonAssignments.Complete
{
    public sealed record CompleteLessonAssignmentCommand(Guid Id , DateOnly EndDate) : IRequest<LessonAssignmentDto>;
}
