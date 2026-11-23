
using Charbagh.Application.TeachingPlans.Common;
using MediatR;

namespace Charbagh.Application.TeachingPlans.Create
{
    public sealed record CreateTeachingPlanCommand(
        Guid LessonAssignmentId,
        Guid BookId,
        Guid StartBookItemId,
        Guid? TargetBookItemId,
        DateOnly StartDate,
        DateOnly? EndDate,
        int DisplayOrder,
        string? Objective,
        string? Notes
        ) : IRequest<TeachingPlanDto>;
}
