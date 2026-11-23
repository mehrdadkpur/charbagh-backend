
using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Common.Exceptions;
using Charbagh.Application.Evaluations.Common;
using MediatR;

namespace Charbagh.Application.Evaluations.SetNotComplete
{
    public sealed class SetSetNotCompletePracticeCompletionStatusEvaluationHandler : IRequestHandler<SetNotCompletePracticeCompletionStatusEvaluationCommand, EvaluationDto>
    {
        private readonly IEvaluationRepository _evaluationRepository;
        private readonly IUnitOfWork _unitOfWork;

        public SetSetNotCompletePracticeCompletionStatusEvaluationHandler(IEvaluationRepository evaluationRepository, IUnitOfWork unitOfWork)
        {

            _evaluationRepository = evaluationRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<EvaluationDto> Handle(SetNotCompletePracticeCompletionStatusEvaluationCommand request, CancellationToken cancellationToken)
        {
            var evaluation = await _evaluationRepository.GetByIdAsync(request.Id, cancellationToken);

            if (evaluation == null)
            {
                throw new EvaluationNotFoundException(request.Id);
            }

            evaluation.SetNotCompletedPracticeCompletion();

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return evaluation.ToDto();
        }
    }

}
