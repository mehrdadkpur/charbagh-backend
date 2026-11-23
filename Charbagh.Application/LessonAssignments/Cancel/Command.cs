
using Charbagh.Application.LessonAssignments.Common;
using MediatR;

namespace Charbagh.Application.LessonAssignments.Cancel
{
    public sealed record CancelLessonAssignmentCommand (Guid Id , DateOnly EndDate) : IRequest<LessonAssignmentDto>;
}
