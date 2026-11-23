namespace Charbagh.Application.Classrooms.Common;

public sealed record ClassroomDto(
    Guid Id,
    Guid BranchId,
    string Name,
    string Code,
    int Capacity,
    string? Description,
    bool IsActive);