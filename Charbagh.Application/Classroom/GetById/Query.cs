using Charbagh.Application.Classrooms.Common;
using MediatR;

namespace Charbagh.Application.Classrooms.GetById;

public sealed record GetClassroomByIdQuery( Guid Id) : IRequest<ClassroomDto>;