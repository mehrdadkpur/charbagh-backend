using Charbagh.Application.PracticeAssignments.Common;
using MediatR;

namespace Charbagh.Application.PracticeAssignments.Update
{
    public sealed record UpdatePracticeAssignmentCommand(
        Guid Id,
        Guid BookItemId,
        string? FromReference,
        string? ToReference,
        string? Instruction,
        DateOnly? DueDate
        ) : IRequest<PracticeAssignmentDto>;
}
