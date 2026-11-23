using Charbagh.Application.TeachingPlans.Common;
using MediatR;

namespace Charbagh.Application.TeachingPlans.Activate
{
    public sealed record ActivateTeachingPlanCommand(Guid Id) : IRequest<TeachingPlanDto>;
}
