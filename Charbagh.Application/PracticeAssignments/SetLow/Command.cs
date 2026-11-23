
using Charbagh.Application.PracticeAssignments.Common;
using MediatR;

namespace Charbagh.Application.PracticeAssignments.SetLow
{
    public sealed record SetLowPriorityPracticeAssignmentCommand(Guid Id) : IRequest<PracticeAssignmentDto>;
}
