using Charbagh.Domain.Entities.Education.StudentEducation;

namespace Charbagh.Application.TeachingPlans.Common
{
    public static class TeachingPlanMapper
    {
        public static TeachingPlanDto ToDto (this TeachingPlan teachingPlanDto)
        {
            return new TeachingPlanDto(
                teachingPlanDto.Id,
                teachingPlanDto.LessonAssignmentId,
                teachingPlanDto.BookId,
                teachingPlanDto.StartBookItemId,
                teachingPlanDto.TargetBookItemId,
                teachingPlanDto.StartDate,
                teachingPlanDto.EndDate,
                teachingPlanDto.DisplayOrder,
                teachingPlanDto.Objective,
                teachingPlanDto.Notes,
                teachingPlanDto.Status
                );
        }
    }
}
