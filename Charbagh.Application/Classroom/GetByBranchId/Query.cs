using Charbagh.Application.Classrooms.Common;
using MediatR;

namespace Charbagh.Application.Classrooms.GetByBranchId;

public sealed record GetClassroomsByBranchIdQuery(Guid BranchId) : IRequest<IReadOnlyList<ClassroomDto>>;