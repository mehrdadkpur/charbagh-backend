
using Charbagh.Application.TeachingPlans.Common;
using MediatR;

namespace Charbagh.Application.TeachingPlans.GetById
{
    public sealed record GetTeachingPlanByIdQuery(Guid Id) : IRequest<TeachingPlanDto>;
}
