
using Charbagh.Application.LessonAssignments.Common;
using MediatR;

namespace Charbagh.Application.LessonAssignments.Resume
{
    public sealed record ResumeLessonAssignmentCommand(Guid Id) : IRequest<LessonAssignmentDto>;
}
