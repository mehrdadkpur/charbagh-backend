using Charbagh.Application.Classrooms.Common;
using MediatR;

namespace Charbagh.Application.Classrooms.Update;

public sealed record UpdateClassroomCommand(
    Guid Id,
    string Name,
    string Code,
    int Capacity,
    string? Description
) : IRequest<ClassroomDto>;