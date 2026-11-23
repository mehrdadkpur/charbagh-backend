
using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Common.Exceptions;
using Charbagh.Application.TeachingPlans.Common;
using MediatR;

namespace Charbagh.Application.TeachingPlans.GetById
{
    public sealed class GetTeachingPlanByIdHandler : IRequestHandler<GetTeachingPlanByIdQuery,TeachingPlanDto>
    {
        private readonly ITeachingPlanRepository _repository;

        public GetTeachingPlanByIdHandler(ITeachingPlanRepository repository)
        {
            _repository = repository;
        }

        public async Task<TeachingPlanDto> Handle (GetTeachingPlanByIdQuery request , CancellationToken cancellationToken)
        {
            var teachingPlan = await _repository.GetByIdAsync(request.Id , cancellationToken);

            if(teachingPlan == null)
            {
                throw new TeachingPlanNotFoundException(request.Id);
            }

            return teachingPlan.ToDto();
        }
    }
}
