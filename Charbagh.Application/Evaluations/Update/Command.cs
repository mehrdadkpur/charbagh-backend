using Charbagh.Application.Evaluations.Common;
using MediatR;

namespace Charbagh.Application.Evaluations.Update
{
    public sealed record UpdateEvaluationCommand(
        Guid Id,
        PracticeCompletionStatus PracticeStatus,
        int? Score,
        string? Strengths,
        string? Weaknesses,
        string? TeacherComment
        ) : IRequest<EvaluationDto>;
}
