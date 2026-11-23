
using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Common.Exceptions;
using Charbagh.Application.Evaluations.Common;
using MediatR;

namespace Charbagh.Application.Evaluations.SetPartiallyComplete
{
    public sealed class SetPartiallyCompletePracticeCompletionStatusEvaluationHandler : IRequestHandler<SetPartiallyCompletePracticeCompletionStatusEvaluationCommand, EvaluationDto>
    {
        private readonly IEvaluationRepository _evaluationRepository;
        private readonly IUnitOfWork _unitOfWork;

        public SetPartiallyCompletePracticeCompletionStatusEvaluationHandler(IEvaluationRepository evaluationRepository, IUnitOfWork unitOfWork)
        {

            _evaluationRepository = evaluationRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<EvaluationDto> Handle(SetPartiallyCompletePracticeCompletionStatusEvaluationCommand request, CancellationToken cancellationToken)
        {
            var evaluation = await _evaluationRepository.GetByIdAsync(request.Id, cancellationToken);

            if (evaluation == null)
            {
                throw new EvaluationNotFoundException(request.Id);
            }

            evaluation.SetPartiallyCompletedPracticeCompletion();

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return evaluation.ToDto();
        }
    }

}
