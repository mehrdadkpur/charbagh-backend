
using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Common.Exceptions;
using Charbagh.Application.TeachingPlans.Common;
using MediatR;

namespace Charbagh.Application.TeachingPlans.Activate
{
    public sealed class ActivateTeachingPlanHandler : IRequestHandler<ActivateTeachingPlanCommand, TeachingPlanDto>
    {
        private readonly ITeachingPlanRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public ActivateTeachingPlanHandler(ITeachingPlanRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<TeachingPlanDto> Handle (ActivateTeachingPlanCommand request , CancellationToken cancellationToken)
        {
            var teachingPlan = await _repository.GetByIdAsync(request.Id , cancellationToken);

            if(teachingPlan == null)
            {
                throw new TeachingPlanNotFoundException(request.Id);
            }

            teachingPlan.Activate();
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return teachingPlan.ToDto();
        }
    }
}
