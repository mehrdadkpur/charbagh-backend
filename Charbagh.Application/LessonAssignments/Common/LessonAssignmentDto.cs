
namespace Charbagh.Application.LessonAssignments.Common
{
    public sealed record LessonAssignmentDto(
        Guid Id,
        Guid StudentId,
        Guid BookId,
        Guid TeacherId,
        Guid InstrumentId,
        DateOnly StartDate,
        DateOnly? EndDate,
        decimal SessionFee,
        PaymentPlanType PaymentPlan,
        string? Notes
        );
}
