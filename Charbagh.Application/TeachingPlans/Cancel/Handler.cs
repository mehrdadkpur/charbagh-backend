using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Common.Exceptions;
using Charbagh.Application.TeachingPlans.Activate;
using Charbagh.Application.TeachingPlans.Common;
using MediatR;

namespace Charbagh.Application.TeachingPlans.Cancel
{
    public sealed class CancelTeachingPlanHandler : IRequestHandler<CancelTeachingPlanCommand, TeachingPlanDto>
    {
        private readonly ITeachingPlanRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public CancelTeachingPlanHandler(ITeachingPlanRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<TeachingPlanDto> Handle(CancelTeachingPlanCommand request, CancellationToken cancellationToken)
        {
            var teachingPlan = await _repository.GetByIdAsync(request.Id, cancellationToken);

            if (teachingPlan == null)
            {
                throw new TeachingPlanNotFoundException(request.Id);
            }

            teachingPlan.Cancel(request.EndDate);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return teachingPlan.ToDto();
        }
    }

}
