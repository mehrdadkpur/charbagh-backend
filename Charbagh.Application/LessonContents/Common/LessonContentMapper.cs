using Charbagh.Domain.Entities.Education.StudentEducation;

namespace Charbagh.Application.LessonContents.Common
{
    public static class LessonContentMapper
    {
        public static LessonContentDto ToDto(this LessonContent lessonContent)
        {
            return new LessonContentDto(
                lessonContent.Id,
                lessonContent.LessonSessionId,
                lessonContent.BookItemId,
                lessonContent.FromReference,
                lessonContent.ToReference,
                lessonContent.TeacherNote
                );
        }
    }
}
