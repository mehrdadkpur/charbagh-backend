
using Charbagh.Application.TeachingPlans.Common;
using MediatR;

namespace Charbagh.Application.TeachingPlans.Complete
{
    public sealed record CompleteTeachingPlanCommand(Guid Id , DateOnly EndDate) : IRequest<TeachingPlanDto>;
}
