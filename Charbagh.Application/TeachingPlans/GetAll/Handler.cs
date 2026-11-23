
using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.TeachingPlans.Common;
using MediatR;

namespace Charbagh.Application.TeachingPlans.GetAll
{
    public sealed class GetAllTeachingPlansHandler : IRequestHandler<GetAllTeachingPlansQuery ,IReadOnlyList< TeachingPlanDto>>
    {
        private readonly ITeachingPlanRepository _repository;

        public GetAllTeachingPlansHandler(ITeachingPlanRepository repository)
        {
            _repository = repository;
        }

        public async Task<IReadOnlyList< TeachingPlanDto>> Handle (GetAllTeachingPlansQuery request , CancellationToken cancellationToken)
        {
            var teachingPlans = await _repository.GetAllAsync (cancellationToken);

            return teachingPlans.Select(x => x.ToDto ()).ToList();
        }
    }
}
