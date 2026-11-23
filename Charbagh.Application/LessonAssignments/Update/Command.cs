using Charbagh.Application.LessonAssignments.Common;
using MediatR;

namespace Charbagh.Application.LessonAssignments.Update
{
    public sealed record UpdateLessonAssignmentCommand(
        Guid Id,
        Guid StudentId,
        Guid TeacherId,
        Guid BookId,
        Guid InstrumentId,
        decimal SessionFee,
        PaymentPlanType PaymentPlan,
        string? Notes
        ) : IRequest<LessonAssignmentDto>;
}
