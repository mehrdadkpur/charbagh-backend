
namespace Charbagh.Application.TeachingPlans.Common
{
    public sealed record TeachingPlanDto (
        Guid Id,
        Guid LessonAssignmentId,
        Guid BookId,
        Guid StartBookItemId,
        Guid? TargetBookItemId,
        DateOnly StartDate,
        DateOnly? EndDate,
        int DisplayOrder,
        string? Objective,
        string? Notes,
        TeachingPlanStatus Status
        );
}
