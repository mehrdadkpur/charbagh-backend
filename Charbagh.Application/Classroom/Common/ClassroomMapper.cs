using Charbagh.Domain.Entities.Branches;

namespace Charbagh.Application.Classrooms.Common;

public static class ClassroomMapper
{
    public static ClassroomDto ToDto(
        this Classroom classroom)
    {
        return new ClassroomDto(
            classroom.Id,
            classroom.BranchId,
            classroom.Name,
            classroom.Code,
            classroom.Capacity,
            classroom.Description,
            classroom.IsActive);
    }
}