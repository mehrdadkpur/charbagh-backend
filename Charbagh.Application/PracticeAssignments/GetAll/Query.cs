
using Charbagh.Application.PracticeAssignments.Common;
using MediatR;

namespace Charbagh.Application.PracticeAssignments.GetAll
{
    public sealed record GetAllPracticeAssignmentsQuery() : IRequest<IReadOnlyList<PracticeAssignmentDto>>;
}
