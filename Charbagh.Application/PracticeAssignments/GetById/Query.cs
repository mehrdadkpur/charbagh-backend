
using Charbagh.Application.PracticeAssignments.Common;
using MediatR;

namespace Charbagh.Application.PracticeAssignments.GetById
{
    public sealed record GetPracticeAssignmentByIdQuery(Guid Id): IRequest<PracticeAssignmentDto>;
}
