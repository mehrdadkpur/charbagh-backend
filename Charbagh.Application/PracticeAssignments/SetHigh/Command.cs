using Charbagh.Application.PracticeAssignments.Common;
using MediatR;

namespace Charbagh.Application.PracticeAssignments.SetHigh
{
    public sealed record SetHighPriorityPracticeAssignmentCommand(Guid Id) : IRequest<PracticeAssignmentDto>;
}
