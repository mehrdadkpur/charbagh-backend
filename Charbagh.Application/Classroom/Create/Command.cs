using Charbagh.Application.Classrooms.Common;
using MediatR;

namespace Charbagh.Application.Classrooms.Create;

public sealed record CreateClassroomCommand(
    Guid BranchId,
    string Name,
    string Code,
    int Capacity,
    string? Description
) : IRequest<ClassroomDto>;