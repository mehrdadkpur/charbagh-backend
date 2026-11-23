
using Charbagh.Application.TeachingPlans.Common;
using MediatR;

namespace Charbagh.Application.TeachingPlans.GetAll
{
    public sealed record GetAllTeachingPlansQuery():IRequest<IReadOnlyList< TeachingPlanDto>>;
}
