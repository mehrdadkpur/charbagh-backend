
using Charbagh.Application.LessonAssignments.Common;
using MediatR;

namespace Charbagh.Application.LessonAssignments.Suspend
{
    public sealed record SuspendLessonAssignmentCommand(Guid Id) : IRequest<LessonAssignmentDto>;
}
