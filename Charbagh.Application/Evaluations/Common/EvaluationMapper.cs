using Charbagh.Domain.Entities.Education.StudentEducation;

namespace Charbagh.Application.Evaluations.Common
{
    public static class EvaluationMapper
    {
        public static EvaluationDto ToDto(this Evaluation evaluation)
        {
            return new EvaluationDto(
               evaluation.Id,
               evaluation.LessonSessionId,
               evaluation.PracticeStatus,
               evaluation.Score,
               evaluation.Strengths,
               evaluation.Weaknesses,
               evaluation.TeacherComment,
               evaluation.EvaluatedAt,
               evaluation.EvaluatedByUserId
                );
        }
    }
}
