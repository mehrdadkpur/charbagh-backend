using Charbagh.Application.Evaluations.Common;
using MediatR;

namespace Charbagh.Application.Evaluations.Create
{
    public sealed record CreateEvaluationCommand(
        Guid LessonSessionId,
        PracticeCompletionStatus PracticeStatus,
        int? Score,
        string? Strengths,
        string? Weaknesses,
        string? TeacherComment,
        Guid EvaluatedByUserId
        ) : IRequest<EvaluationDto>;
}
