using Charbagh.Application.LessonAssignments.Common;
using MediatR;

namespace Charbagh.Application.LessonAssignments.Update
{
    public sealed record UpdateLessonAssignmentCommand(
        Guid Id,
        Guid TeacherId,
        Guid BookId,
        decimal SessionFee,
        PaymentPlanType PaymentPlan,
        string? Notes
        ) : IRequest<LessonAssignmentDto>;
}
