
using Charbagh.Application.PracticeAssignments.Common;
using MediatR;

namespace Charbagh.Application.PracticeAssignments.SetNormal
{
    public sealed record SetNormalPriorityPracticeAssignmentCommand(Guid Id) : IRequest<PracticeAssignmentDto>;
}
