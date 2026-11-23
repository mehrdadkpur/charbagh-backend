
using Charbagh.Application.LessonAssignments.Common;
using MediatR;

namespace Charbagh.Application.LessonAssignments.Create
{
    public sealed record CreateLessonAssignmentCommand(
        Guid StudentId,
        Guid BookId,
        Guid TeacherId,
        Guid InstrumentId,
        DateOnly StartDate,
        decimal SessionFee,
        PaymentPlanType PaymentPlan,
        string? Notes
        ) :IRequest<LessonAssignmentDto>;
}
