using Charbagh.Application.TeachingPlans.Common;
using MediatR;

namespace Charbagh.Application.TeachingPlans.Cancel
{
    public sealed record CancelTeachingPlanCommand(Guid Id , DateOnly EndDate) : IRequest<TeachingPlanDto>;
}
