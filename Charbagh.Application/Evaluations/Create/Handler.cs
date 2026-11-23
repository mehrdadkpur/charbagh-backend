
using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Common.Exceptions;
using Charbagh.Application.Evaluations.Common;
using Charbagh.Domain.Entities.Education.StudentEducation;
using MediatR;

namespace Charbagh.Application.Evaluations.Create
{
    public sealed class CreateEvaluationHandler : IRequestHandler<CreateEvaluationCommand,EvaluationDto>
    {
        private readonly IEvaluationRepository _evaluationRepository;
        private readonly ILessonSessionRepository _lessonSessionRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateEvaluationHandler(IEvaluationRepository evaluationRepository , ILessonSessionRepository lessonSessionRepository , IUnitOfWork unitOfWork)
        {
            _evaluationRepository = evaluationRepository;
            _lessonSessionRepository = lessonSessionRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task <EvaluationDto> Handle (CreateEvaluationCommand request , CancellationToken cancellationToken)
        {
            var lessonSession = await _lessonSessionRepository.GetByIdAsync(request.LessonSessionId, cancellationToken);

            if (lessonSession == null)
            {
                throw new LessonSessionNotFoundException(request.LessonSessionId);
            }

            var existingEvaluation = await _evaluationRepository.GetByLessonSessionIdAsync( request.LessonSessionId, cancellationToken);

                if (existingEvaluation is not null)
                {
                    throw new DuplicateEvaluationException(request.LessonSessionId);
                }

            var evaluation = Evaluation.Create(
                request.LessonSessionId,
                request.PracticeStatus,
                request.Score,
                request.Strengths,
                request.Weaknesses,
                request.TeacherComment,
                DateTime.UtcNow,
                request.EvaluatedByUserId // بعدا باید توسط کاربر لاگین شده کامل شود
                );

            await _evaluationRepository.AddAsync(evaluation, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return evaluation.ToDto();
        }
    }
}
