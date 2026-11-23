namespace Charbagh.Application.Evaluations.Common
{
    public sealed record EvaluationDto(
        Guid Id,
        Guid LessonSessionId,
        PracticeCompletionStatus PracticeStatus,
        int? Score,
        string? Strengths,
        string? Weaknesses,
        string? TeacherComment,
        DateTime EvaluatedAt,
        Guid EvaluatedByUserId
        );

}
