
using Charbagh.Application.TeachingPlans.Common;
using MediatR;

namespace Charbagh.Application.TeachingPlans.Update
{
    public sealed record UpdateTeachingPlanCommand(
        Guid Id,
        Guid StartBookItemId,
        Guid? TargetBookItemId,
        DateOnly StartDate,
        DateOnly? EndDate,
        int DisplayOrder,
        string? Objective,
        string? Notes
        ) : IRequest<TeachingPlanDto>;
}
