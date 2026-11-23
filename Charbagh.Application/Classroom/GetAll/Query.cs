using Charbagh.Application.Classrooms.Common;
using MediatR;

namespace Charbagh.Application.Classrooms.GetAll;

public sealed record GetAllClassroomsQuery : IRequest<IReadOnlyList<ClassroomDto>>;