using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Common.Exceptions;
using Charbagh.Application.Evaluations.Common;
using Charbagh.Application.Evaluations.SetNotAssigned;
using MediatR;

namespace Charbagh.Application.Evaluations.SetComplete
{
    public sealed class SetCompletePracticeCompletionStatusEvaluationHandler : IRequestHandler<SetCompletePracticeCompletionStatusEvaluationCommand, EvaluationDto>
    {
        private readonly IEvaluationRepository _evaluationRepository;
        private readonly IUnitOfWork _unitOfWork;

        public SetCompletePracticeCompletionStatusEvaluationHandler(IEvaluationRepository evaluationRepository, IUnitOfWork unitOfWork)
        {

            _evaluationRepository = evaluationRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<EvaluationDto> Handle(SetCompletePracticeCompletionStatusEvaluationCommand request, CancellationToken cancellationToken)
        {
            var evaluation = await _evaluationRepository.GetByIdAsync(request.Id, cancellationToken);

            if (evaluation == null)
            {
                throw new EvaluationNotFoundException(request.Id);
            }

            evaluation.SetCompletePracticeCompletion();

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return evaluation.ToDto();
        }
    }

}
