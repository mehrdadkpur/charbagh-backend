
using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Common.Exceptions;
using Charbagh.Application.Evaluations.Common;
using Charbagh.Application.Evaluations.SetNotAssigned;
using MediatR;

namespace Charbagh.Application.Evaluations.NotAssigned
{
    public sealed class SetNotAssignedPracticeCompletionStatusEvaluationHandler : IRequestHandler<SetNotAssignedPracticeCompletionStatusEvaluationCommand, EvaluationDto>
    {
        private readonly IEvaluationRepository _evaluationRepository;
        private readonly IUnitOfWork _unitOfWork;

        public SetNotAssignedPracticeCompletionStatusEvaluationHandler(IEvaluationRepository evaluationRepository, IUnitOfWork unitOfWork)
        {

            _evaluationRepository = evaluationRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<EvaluationDto> Handle (SetNotAssignedPracticeCompletionStatusEvaluationCommand request , CancellationToken cancellationToken)
        {
            var evaluation = await _evaluationRepository.GetByIdAsync(request.Id, cancellationToken);

            if (evaluation == null)
            {
                throw new EvaluationNotFoundException(request.Id);
            }

            evaluation.SetNotAssignedPracticeCompletion();

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return evaluation.ToDto();
        }
    }
}
