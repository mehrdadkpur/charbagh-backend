
using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Common.Exceptions;
using Charbagh.Application.Evaluations.Common;
using MediatR;

namespace Charbagh.Application.Evaluations.Update
{
    public sealed class UpdateEvaluationHandler : IRequestHandler<UpdateEvaluationCommand, EvaluationDto>
    {
        private readonly IEvaluationRepository _evaluationRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateEvaluationHandler(IEvaluationRepository evaluationRepository, IUnitOfWork unitOfWork)
        {
            _evaluationRepository = evaluationRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<EvaluationDto> Handle(UpdateEvaluationCommand request, CancellationToken cancellationToken)
        {
            var evaluation = await _evaluationRepository.GetByIdAsync(request.Id, cancellationToken);

            if (evaluation == null)
            {
                throw new EvaluationNotFoundException(request.Id);
            }



            evaluation.Update(
                request.PracticeStatus,
                request.Score,
                request.Strengths,
                request.Weaknesses,
                request.TeacherComment
                );

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return evaluation.ToDto();
        }
    }

}
